-- craftersmine LVM managed machine sandbox code

-- Import required C# Native namespaces
import('craftersmine.LVM.Core', 'craftersmine.LVM.Core.BaseLibraries');   -- C# classes as libraries such as Component, Computer, etc.

--- Table for sandbox
sandboxGlobals = {
	pcall = function (f, ...)
		return spcall(f, ...);
	end,
	xpcall = function (f, msgh, ...)
		return sxpcall(f, msgh, ...);
	end,
	pairs = pairs,
	ipairs = ipairs,
	dofile = nil,
	error = error,
	_G = nil,
	getmetatable = getmetatable,
	load = function (chunk, name, mode, env)
		return load(chunk, name, mode, env or sandboxGlobals)
	end,
	loadfile = nil,
	next = next,
	rawequal = rawequal,
	rawget = rawget,
	rawset = rawset,
	select = select,
	setmetatable = setmetatable,
	tonumber = tonumber,
	tostring = tostring,
	type = type,
	_VERSION = _VERSION,

	coroutine = {
		create = coroutine.create,
		resume = coroutine.resume,
		running = coroutine.running,
		status = coroutine.status,
		wrap = coroutine.wrap,
		yield = coroutine.yield,
		isyieldable = coroutine.isyieldable
	},

	string = {
		byte = string.byte,
		char = string.char,
		dump = string.dump,
		find = string.find,
		format = string.format,
		gmatch = string.gmatch,
		gsub = string.gsub,
		len = string.len,
		lower = string.lower,
		match = string.match,
		rep = string.rep,
		reverse = string.reverse,
		sub = string.sub,
		pack = string.pack,
		unpack = string.unpack,
		packsize = string.packsize
	},

	table = {
		concat = table.concat,
		insert = table.insert,
		pack = table.pack,
		remove = table.remove,
		sort = table.sort,
		unpack = table.unpack,
		move = table.move
	},

	math = {
		abs = math.abs,
		acos = math.acos,
		asin = math.asin,
		atan = math.atan,
		atan2 = math.atan2,
		ceil = math.ceil,
		cos = math.cos,
		cosh = math.cosh,
		deg = math.deg,
		exp = math.exp,
		floor = math.floor,
		fmod = math.fmod,
		frexp = math.frexp,
		huge = math.huge,
		ldexp = math.ldexp,
		log = math.log,
		max = math.max,
		min = math.min,
		modf = math.modf,
		pi = math.pi,
		pow = math.pow or function (a, b)
			return a ^ b;
		end,
		rad = math.rad,
		random = function (...)
			return spcall(math.random, ...);
		end,
		randomseed = function (seed)
			return spcall(math.randomseed, seed);
		end,
		sin = math.sin,
		sinh = math.sinh,
		sqrt = math.sqrt,
		tan = math.tan,
		maxinteger = math.maxinteger,
		mininteger = math.mininteger,
		tointeger = math.tointeger,
		type = math.type,
		ult = math.ult
	},

	-- Deprecated in Lua 5.3
	bit32 = bit32 and {
		arshift = bit32.arshift,
		band = bit32.band,
		bnot = bit32.bnot,
		bor = bit32.bor,
		btest = bit32.btest,
		bxor = bit32.bxor,
		extract = bit32.extract,
		replace = bit32.replace,
		lrotate = bit32.lrotate,
		lshift = bit32.lshift,
		rrotate = bit32.rrotate,
		rshift = bit32.rshift
	},

	io = nil,

	os = {
		clock = os.clock,
		date = function (format, time)
			return spcall(os.date, format, time);
		end,
		difftime = function (time1, time2)
			return time2 - time1;
		end,
		execute = nil,
		exit = nil,
		remove = nil,
		rename = nil,
		time = function (tbl)
			checkArg(1, tbl, "table", "nil");
			return os.time(table);
		end,
		tmpname = nil
	},

	debug = {
		getinfo = function (...)
			local result = debug.getinfo(...);
			if result then
				return {
					source = result.source,
					short_src = result.short_src,
					linedefined = result.linedefined,
					lastlinedefined = result.lastlinedefined,
					what = result.what,
					currentline = result.currentline,
					nups = result.nups,
					nparams = result.nparams,
					isvararg = result.isvararg,
					name = result.name,
					namewhat = result.namewhat,
					istailcall = result.istailcall
				}
			end
		end,
		traceback = debug.traceback,
		getlocal = function (...) return (debug.getlocal(...)) end,
		getupvalue = function (...) return (debug.getupvalue(...)) end
	},

	utf8 = utf8 and {
		char = utf8.char,
		charpattern = utf8.charpattern,
		codes = utf8.codes,
		codepoint = utf8.codepoint,
		len = utf8.len,
		offset = utf8.offset
	},

	checkArg = checkArg
}

--- Contains metamethods for component proxy callbacks
local componentCallback = {
	__call = function (self, ...)
		return libComponent.invoke(self.address, self.name, ...);
	end,
	__tostring = function (self)
		return libComponent.doc(self.address, self.name) or "function";
	end
}

--- Contains metamethods for component proxy
local componentProxy = {
	__index = function (self, key)
		if self.fields[key] and self.fields[key].getter then 
			return libComponent.getFieldValue(self.address, key);
		else
			rawget(self, key);
		end
	end,
	__newindex = function (self, key, value)
		if self.fields[key] and self.fields[key].setter then
			libComponent.setFieldValue(self.address, key, value)
		  elseif self.fields[key] and self.fields[key].getter then
			error("field is read-only")
		  else
			rawset(self, key, value)
		  end
	end,
	__pairs = function(self)
		local keyProxy, keyField, value
		return function()
		  if not keyField then
			repeat
			  keyProxy, value = next(self, keyProxy)
			until not keyProxy or keyProxy ~= "fields"
		  end
		  if not keyProxy then
			keyField, value = next(self.fields, keyField)
		  end
		  return keyProxy or keyField, value
		end
	  end
}

--- Machine component library
libComponent = {
	--- Gets and returns basic documentation about functions
	---@param address string
	---@param method string
	---@return string
	doc = function (address, method)
		checkArg(1, address, "string");
		checkArg(2, method, "string");
		return Component.doc(address, method);
	end,
	--- Invokes component method and returns it's values
	---@param address string
	---@param method string
	---@return any
	invoke = function (address, method, ...)
		checkArg(1, address, "string");
		checkArg(2, method, "string");
		return table.unpack(Component.invoke(address, method, ...));
	end,
	--- Returns a list of connected components to machine, also have ability to be iterated through __call
	---@param filter string
	---@param exact boolean
	---@return table
	list = function (filter, exact)
		checkArg(1, filter, "string", "nil");
		if filter == nil then filter = ""; end
		if exact == nil then exact = false; end
		local list = spcall(Component.list, filter, exact);
		local key = nil;
		return setmetatable(list, {
			__call = function ()
				key = next(list, key);
				if key then
					return key, list[key];
				end
			end
		})
	end,
	--- Gets available methods of component under specified address
	---@param address string
	---@return table | boolean
	---@return string
	methods = function (address)
		local result, reason = spcall(Component.methods, address);
		if type(result) == "table" then
			for k, v in pairs(result) do
				if not v.setter and not v.getter then
					result[k] = v.direct;
				else
					result[k] = nil;
				end
			end
			return result;
		end
		return result, reason;
	end,
	--- Gets available fields of component under specified address
	---@param address string
	---@return table | boolean
	---@return string
	fields = function (address)
		local result, reason = spcall(Component.methods, address);
		if type(result) == "table" then
			for k, v in pairs(result) do
				if not v.setter and not v.getter then result[k] = nil end;
			end
			return result;
		end
		return result, reason;
	end,
	--- Gets component type under specified address
	---@param address string
	---@return string
	type = function (address)
		return spcall(Component.type, address);
	end,
	--- Gets a value of specified field of component under specified address
	---@param address string
	---@param field string
	---@return any
	getFieldValue = function (address, field)
		checkArg(1, address, "string");
		checkArg(2, field, "string");
		return Component.getFieldValue(address, field);
	end,
	--- Sets a value of specified field of component under specified address
	---@param address string
	---@param field string
	---@param value any
	setFieldValue = function (address, field, value)
		checkArg(1, address, "string");
		checkArg(2, field, "string");
		Component.setFieldValue(address, field, value);
	end,
	--- Getsets a proxy of component under specified address
	---@param address string
	---@return table
	proxy = function (address)
		local type, reason = spcall(Component.type, address);
		if not type then return nil, reason end;

		local proxy = { address = address, type = type, fields = {} };

		local methods, reason = spcall(Component.methods, address);
		if not methods then return nil, reason end;
		for method, info in pairs(methods) do
			for key, value in pairs(info) do
			end
			if not info.getter and not info.setter then 
				proxy[method] = setmetatable({ address = address, name = method }, componentCallback);
			else
				proxy.fields[method] = info;
			end
		end

		setmetatable(proxy, componentProxy);
		return proxy;
	end
}

--- Machine library
libComputer = {
	address = Computer.address,
	setBootAddress = function (address)
		libComponent.invoke(libComponent.list("eeprom")(), "setParam", "boot_device", address);
	end,
	getBootAddress = function ()
		libComponent.invoke(libComponent.list("eeprom")(), "getParam", "boot_device");
	end,
	beep = function (...)
		libComponent.invoke(computer.address, "beep", ...);
	end,
	shutdown = function (reboot)
		if reboot then 
			spcall(Computer.reboot())
		else spcall(Computer.shutdown()) end
	end,
	pushSignal = function (name, ...)
		return spcall(Computer.pushSignal(name, ...));
	end,
	pullSignal = function ()
		local val = spcall(Computer.pullSignal());
		return val["name"], table.unpack(val, 1, val.n - 1);
	end
}

--- Checks the argument for specific type and returns true if type equal or throws an error
---@param n number
---@param have any
function checkArg(n, have, ...)
	have = type(have)
	local function check(want, ...)
	  if not want then
		return false
	  else
		return have == want or check(...)
	  end
	end
	if not check(...) then
	  local msg = string.format("bad argument #%d (%s expected, got %s)",
								n, table.concat({...}, " or "), have)
	  error(msg, 3)
	end
  end

--- Calls a function and returns it value if no error occured, otherwise returns false and error message
---@param f function
---@return boolean
---@return any
function spcall(f, ...)
	result = table.pack(pcall(f, ...));
	if not result[1] then
		if type(result[2]) == "userdata" then
			if (result[2].GetType().Name == "LuaMachineException") then
				return false, result[2].InnerException.LuaErrorMessage;
			else 
				return false, result[2].InnerException.Message;
			end
		else return table.unpack(result) end
	else
		return table.unpack(result, 2);
	end
end

--- Calls a function in protected mode and if error occured returns false and calls msgh function
---@param f function
---@param msgh function
---@return boolean
---@return any
function sxpcall(f, msgh, ...)
	result = table.pack(spcall(f, ...));
	if not result[1] then 
		msghret = msgh();
		return false, msghret, table.unpack(result, 3);
	else
		return table.unpack(result);
	end
end

--- Loads EEPROM code and returns coroutine to be run
---@param sandbox table
---@return thread
function bootstrap(sandbox)
	log("INFO/MACHINE-BOOTSTRAP", "Trying to get EEPROM component...");
	local eeprom = libComponent.list("eeprom")();
	if eeprom then
		log("INFO/MACHINE-BOOTSTRAP", "Trying to get EEPROM code...");
		local eepromCode = libComponent.invoke(eeprom, "get");
		if eepromCode and #eepromCode > 0 then
			log("INFO/MACHINE-BOOTSTRAP", "Loading EEPROM code...");
			local biosFunc, reason = load(eepromCode, "=bios", "t", sandbox);
			if biosFunc then
				log("DONE/MACHINE-BOOTSTRAP", "EEPROM code loaded!");
				return coroutine.create(biosFunc);
			end
			error("failed to load bios: " .. reason);
		end
	end
	error("no eeprom found; install correct EEPROM");
end

--- Main machine entrypoint
--- machineArguments[1] = string | EEPROM Lua code
--- machineArguments[2] = boolean | Is debug mode enabled
---@param machineArguments table
local function main(machineArguments)
	log("INFO/MACHINE", "Initializing machine...");
	debugMode = false;

	-- get arguments
	local eepromCode = machineArguments[1];
	local debugMode = machineArguments[2];

	sandboxGlobals.component = libComponent;
	sandboxGlobals.computer = libComputer;
		
	sandboxGlobals._G = sandboxGlobals

	-- if debugMode enabled, iterate through sandbox globals table
	if debugMode then
		log("INFO/MACHINE", "Debug mode is enabled!");
		for k,v in pairs(sandboxGlobals) do 
			print(k, v);
		end
	end

	log("INFO/MACHINE", "Bootstrapping machine...");
	local co = bootstrap(sandboxGlobals);
	log("INFO/MACHINE", "Machine coroutine running...");
	local result = table.pack(coroutine.resume(co));
	log("INFO/MACHINE", "Machine coroutine stopped");
	if not result then 
		log("ERROR/MACHINE", "Machine execution ended with error!");
		error(tostring(result[2]), 0);
	elseif coroutine.status(co) == "dead" then
		log("INFO/MACHINE", "Machine execution ended");
		error("machine halted");
	end

	-- "00000000-0000-0000-0000-000000000000"  -- debugging component address
end


-- Initialize machine, that's where managed code ends and starts unmanaged Lua code
main(table.pack(...));