﻿next_reg_id = 0;
export next_reg_id;

intmap("libtysila4.target", "Target", "rt", "rt_map", "init_rtmap", [
	"gpr",
	"float",
	"stack",
	"contents",
	"multi"
]);

export function Ri32(string name)
{
	ret = [ name = name, id = next_reg_id, type = rt_gpr, size = 32, mask = 1 << next_reg_id ];
	next_reg_id = next_reg_id + 1;
	export next_reg_id;
	vargen(name, ret);
	vargen(name + "reg", ret.mask);
	
	return ret;
}

export function Rf32(string name)
{
	ret = [ name = name, id = next_reg_id, type = rt_float, size = 32, mask = 1 << next_reg_id ];
	next_reg_id = next_reg_id + 1;
	export next_reg_id;
	vargen(name, ret);
	vargen(name + "reg", ret.mask);
	
	return ret;
}

export function MultiReg(string name, array regs)
{
	mask = 0;
	foreach(reg in regs)
	{
		mask = mask | reg.mask;
		export mask;
	}

	ret = [ name = name, id = next_reg_id, type = rt_multi, size = regs[0].size, mask = mask ];
	next_reg_id = next_reg_id + 1;
	export next_reg_id;
	vargen(name, ret);
	vargen(name + "reg", ret.mask);
	
	return ret;

}

export function Ri64(string name)
{
	ret = [ name = name, id = next_reg_id, type = rt_gpr, size = 64, mask = 1 << next_reg_id ];
	next_reg_id = next_reg_id + 1;
	export next_reg_id;
	vargen(name, ret);
	vargen(name + "reg", ret.mask);
	
	return ret;
}

export function Rf64(string name)
{
	ret = [ name = name, id = next_reg_id, type = rt_float, size = 64, mask = 1 << next_reg_id ];
	next_reg_id = next_reg_id + 1;
	export next_reg_id;
	vargen(name, ret);
	vargen(name + "reg", ret.mask);
	
	return ret;
}

export function Rstack()
{
	ret = [ name = "stack", id = next_reg_id, type = rt_stack, size = 0, mask = 1 << next_reg_id ];
	next_reg_id = next_reg_id + 1;
	export next_reg_id;
	vargen(ret.name, ret);
	vargen(ret.name + "reg", ret.mask);

	return ret;
}

export function Rcontents()
{
	ret = [ name = "contents", id = next_reg_id, type = rt_contents, size = 0, mask = 1 << next_reg_id ];
	next_reg_id = next_reg_id + 1;
	export next_reg_id;
	vargen(ret.name, ret);
	vargen(ret.name + "reg", ret.mask);

	return ret;
}

