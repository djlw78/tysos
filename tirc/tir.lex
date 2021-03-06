%namespace TIRParse
%visibility internal

%{
	StringBuilder sb;
%}

%%

[0-9]+			yylval.intval = Int32.Parse(yytext); return (int)Tokens.INT;
"="				return (int)Tokens.EQUALS;
"("				return (int)Tokens.LPAREN;
")"				return (int)Tokens.RPAREN;
","				return (int)Tokens.COMMA;
"*"				return (int)Tokens.MUL;
"+"				return (int)Tokens.PLUS;
"-"				return (int)Tokens.MINUS;
"&"				return (int)Tokens.AMP;
"$"				return (int)Tokens.DOLLARS;
":"				return (int)Tokens.COLON;
"\n"			return (int)Tokens.NEWLINE;
"["				return (int)Tokens.LBRACK;
"]"				return (int)Tokens.RBRACK;
"."				return (int)Tokens.DOT;
"<"				return (int)Tokens.LT;
">"				return (int)Tokens.GT;

func			return (int)Tokens.FUNC;
extern			return (int)Tokens.EXTERN;
with			return (int)Tokens.WITH;
def				return (int)Tokens.DEF;
as				return (int)Tokens.AS;

invalid        return (int)Tokens.INVALID;
ldconst_i4        return (int)Tokens.LDCONST_I4;
ldconst_i8        return (int)Tokens.LDCONST_I8;
ldconst_r4        return (int)Tokens.LDCONST_R4;
ldconst_r8        return (int)Tokens.LDCONST_R8;
ldconst_i        return (int)Tokens.LDCONST_I;
add_i4        return (int)Tokens.ADD_I4;
add_i8        return (int)Tokens.ADD_I8;
add_r8        return (int)Tokens.ADD_R8;
add_r4        return (int)Tokens.ADD_R4;
add_i        return (int)Tokens.ADD_I;
add_ovf_i4        return (int)Tokens.ADD_OVF_I4;
add_ovf_i8        return (int)Tokens.ADD_OVF_I8;
add_ovf_i        return (int)Tokens.ADD_OVF_I;
add_ovf_un_i4        return (int)Tokens.ADD_OVF_UN_I4;
add_ovf_un_i8        return (int)Tokens.ADD_OVF_UN_I8;
add_ovf_un_i        return (int)Tokens.ADD_OVF_UN_I;
and_i4        return (int)Tokens.AND_I4;
and_i8        return (int)Tokens.AND_I8;
and_i        return (int)Tokens.AND_I;
arglist        return (int)Tokens.ARGLIST;
cmp_i4        return (int)Tokens.CMP_I4;
cmp_i8        return (int)Tokens.CMP_I8;
cmp_i        return (int)Tokens.CMP_I;
cmp_r8        return (int)Tokens.CMP_R8;
cmp_r4        return (int)Tokens.CMP_R4;
cmp_r8_un        return (int)Tokens.CMP_R8_UN;
cmp_r4_un        return (int)Tokens.CMP_R4_UN;
br        return (int)Tokens.BR;
beq        return (int)Tokens.BEQ;
bne        return (int)Tokens.BNE;
bg        return (int)Tokens.BG;
bge        return (int)Tokens.BGE;
bl        return (int)Tokens.BL;
ble        return (int)Tokens.BLE;
ba        return (int)Tokens.BA;
bae        return (int)Tokens.BAE;
bb        return (int)Tokens.BB;
bbe        return (int)Tokens.BBE;
br_ehclause        return (int)Tokens.BR_EHCLAUSE;
throweq        return (int)Tokens.THROWEQ;
throwne        return (int)Tokens.THROWNE;
throw_ovf        return (int)Tokens.THROW_OVF;
throw_ovf_un        return (int)Tokens.THROW_OVF_UN;
throwge_un        return (int)Tokens.THROWGE_UN;
throwg_un        return (int)Tokens.THROWG_UN;
break_        return (int)Tokens.BREAK_;
throw_        return (int)Tokens.THROW_;
ldftn        return (int)Tokens.LDFTN;
call_i4        return (int)Tokens.CALL_I4;
call_i8        return (int)Tokens.CALL_I8;
call_i        return (int)Tokens.CALL_I;
call_r4        return (int)Tokens.CALL_R4;
call_r8        return (int)Tokens.CALL_R8;
call_void        return (int)Tokens.CALL_VOID;
call_vt        return (int)Tokens.CALL_VT;
seteq        return (int)Tokens.SETEQ;
setne        return (int)Tokens.SETNE;
setg        return (int)Tokens.SETG;
setge        return (int)Tokens.SETGE;
setl        return (int)Tokens.SETL;
setle        return (int)Tokens.SETLE;
seta        return (int)Tokens.SETA;
setae        return (int)Tokens.SETAE;
setb        return (int)Tokens.SETB;
setbe        return (int)Tokens.SETBE;
examinef        return (int)Tokens.EXAMINEF;
brfinite        return (int)Tokens.BRFINITE;
conv_i4_u1zx        return (int)Tokens.CONV_I4_U1ZX;
conv_i4_i1sx        return (int)Tokens.CONV_I4_I1SX;
conv_i4_u2zx        return (int)Tokens.CONV_I4_U2ZX;
conv_i4_i2sx        return (int)Tokens.CONV_I4_I2SX;
conv_i4_i8sx        return (int)Tokens.CONV_I4_I8SX;
conv_i4_u8zx        return (int)Tokens.CONV_I4_U8ZX;
conv_i4_isx        return (int)Tokens.CONV_I4_ISX;
conv_i4_uzx        return (int)Tokens.CONV_I4_UZX;
conv_i8_u1zx        return (int)Tokens.CONV_I8_U1ZX;
conv_i8_i1sx        return (int)Tokens.CONV_I8_I1SX;
conv_i8_u2zx        return (int)Tokens.CONV_I8_U2ZX;
conv_i8_i2sx        return (int)Tokens.CONV_I8_I2SX;
conv_i8_i4sx        return (int)Tokens.CONV_I8_I4SX;
conv_i8_u4zx        return (int)Tokens.CONV_I8_U4ZX;
conv_i8_isx        return (int)Tokens.CONV_I8_ISX;
conv_i8_uzx        return (int)Tokens.CONV_I8_UZX;
conv_i_u1zx        return (int)Tokens.CONV_I_U1ZX;
conv_i_i1sx        return (int)Tokens.CONV_I_I1SX;
conv_i_u2zx        return (int)Tokens.CONV_I_U2ZX;
conv_i_i2sx        return (int)Tokens.CONV_I_I2SX;
conv_i_i8sx        return (int)Tokens.CONV_I_I8SX;
conv_i_u8zx        return (int)Tokens.CONV_I_U8ZX;
conv_i_isx        return (int)Tokens.CONV_I_ISX;
conv_i_uzx        return (int)Tokens.CONV_I_UZX;
conv_i_i4sx        return (int)Tokens.CONV_I_I4SX;
conv_i_u4zx        return (int)Tokens.CONV_I_U4ZX;
conv_r8_i8        return (int)Tokens.CONV_R8_I8;
conv_r8_i4        return (int)Tokens.CONV_R8_I4;
conv_r8_i        return (int)Tokens.CONV_R8_I;
conv_i4_r8        return (int)Tokens.CONV_I4_R8;
conv_i8_r8        return (int)Tokens.CONV_I8_R8;
conv_i_r8        return (int)Tokens.CONV_I_R8;
conv_r4_i8        return (int)Tokens.CONV_R4_I8;
conv_r4_i4        return (int)Tokens.CONV_R4_I4;
conv_r4_i        return (int)Tokens.CONV_R4_I;
conv_i4_r4        return (int)Tokens.CONV_I4_R4;
conv_i8_r4        return (int)Tokens.CONV_I8_R4;
conv_i_r4        return (int)Tokens.CONV_I_R4;
conv_r8_r4        return (int)Tokens.CONV_R8_R4;
conv_r4_r8        return (int)Tokens.CONV_R4_R8;
conv_u4_r8        return (int)Tokens.CONV_U4_R8;
conv_u8_r8        return (int)Tokens.CONV_U8_R8;
conv_u_r8        return (int)Tokens.CONV_U_R8;
movstring        return (int)Tokens.MOVSTRING;
div_i4        return (int)Tokens.DIV_I4;
div_i8        return (int)Tokens.DIV_I8;
div_i        return (int)Tokens.DIV_I;
div_r8        return (int)Tokens.DIV_R8;
div_r4        return (int)Tokens.DIV_R4;
div_u4        return (int)Tokens.DIV_U4;
div_u8        return (int)Tokens.DIV_U8;
div_u        return (int)Tokens.DIV_U;
setstring_value        return (int)Tokens.SETSTRING_VALUE;
getstring_value        return (int)Tokens.GETSTRING_VALUE;
storestring        return (int)Tokens.STORESTRING;
jmpmethod        return (int)Tokens.JMPMETHOD;
ldobj_i4        return (int)Tokens.LDOBJ_I4;
ldobj_i8        return (int)Tokens.LDOBJ_I8;
ldobj_r4        return (int)Tokens.LDOBJ_R4;
ldobj_r8        return (int)Tokens.LDOBJ_R8;
ldobj_i        return (int)Tokens.LDOBJ_I;
ldobj_vt        return (int)Tokens.LDOBJ_VT;
ldobja_ex_i        return (int)Tokens.LDOBJA_EX_I;
stobj_i4        return (int)Tokens.STOBJ_I4;
stobj_i8        return (int)Tokens.STOBJ_I8;
stobj_r4        return (int)Tokens.STOBJ_R4;
stobj_r8        return (int)Tokens.STOBJ_R8;
stobj_i        return (int)Tokens.STOBJ_I;
stobj_vt        return (int)Tokens.STOBJ_VT;
ldarga        return (int)Tokens.LDARGA;
ldloca        return (int)Tokens.LDLOCA;
ldstra        return (int)Tokens.LDSTRA;
lddataa        return (int)Tokens.LDDATAA;
mul_i4        return (int)Tokens.MUL_I4;
mul_i8        return (int)Tokens.MUL_I8;
mul_i        return (int)Tokens.MUL_I;
mul_r8        return (int)Tokens.MUL_R8;
mul_r4        return (int)Tokens.MUL_R4;
mul_ovf_i4        return (int)Tokens.MUL_OVF_I4;
mul_ovf_i8        return (int)Tokens.MUL_OVF_I8;
mul_ovf_i        return (int)Tokens.MUL_OVF_I;
mul_ovf_un_i4        return (int)Tokens.MUL_OVF_UN_I4;
mul_ovf_un_i8        return (int)Tokens.MUL_OVF_UN_I8;
mul_ovf_un_i        return (int)Tokens.MUL_OVF_UN_I;
mul_un_i4        return (int)Tokens.MUL_UN_I4;
mul_un_i8        return (int)Tokens.MUL_UN_I8;
mul_un_i        return (int)Tokens.MUL_UN_I;
neg_i4        return (int)Tokens.NEG_I4;
neg_i8        return (int)Tokens.NEG_I8;
neg_i        return (int)Tokens.NEG_I;
neg_r8        return (int)Tokens.NEG_R8;
neg_r4        return (int)Tokens.NEG_R4;
not_i4        return (int)Tokens.NOT_I4;
not_i8        return (int)Tokens.NOT_I8;
not_i        return (int)Tokens.NOT_I;
or_i4        return (int)Tokens.OR_I4;
or_i8        return (int)Tokens.OR_I8;
or_i        return (int)Tokens.OR_I;
rem_i4        return (int)Tokens.REM_I4;
rem_i8        return (int)Tokens.REM_I8;
rem_i        return (int)Tokens.REM_I;
rem_r8        return (int)Tokens.REM_R8;
rem_r4        return (int)Tokens.REM_R4;
rem_un_i4        return (int)Tokens.REM_UN_I4;
rem_un_i8        return (int)Tokens.REM_UN_I8;
rem_un_i        return (int)Tokens.REM_UN_I;
ret_void        return (int)Tokens.RET_VOID;
ret_i4        return (int)Tokens.RET_I4;
ret_i8        return (int)Tokens.RET_I8;
ret_i        return (int)Tokens.RET_I;
ret_r8        return (int)Tokens.RET_R8;
ret_vt        return (int)Tokens.RET_VT;
shl_i4        return (int)Tokens.SHL_I4;
shl_i8        return (int)Tokens.SHL_I8;
shl_i        return (int)Tokens.SHL_I;
shr_i4        return (int)Tokens.SHR_I4;
shr_i8        return (int)Tokens.SHR_I8;
shr_i        return (int)Tokens.SHR_I;
shr_un_i4        return (int)Tokens.SHR_UN_I4;
shr_un_i8        return (int)Tokens.SHR_UN_I8;
shr_un_i        return (int)Tokens.SHR_UN_I;
sub_i4        return (int)Tokens.SUB_I4;
sub_i8        return (int)Tokens.SUB_I8;
sub_i        return (int)Tokens.SUB_I;
sub_r8        return (int)Tokens.SUB_R8;
sub_r4        return (int)Tokens.SUB_R4;
sub_ovf_i        return (int)Tokens.SUB_OVF_I;
sub_ovf_un_i        return (int)Tokens.SUB_OVF_UN_I;
switch_        return (int)Tokens.SWITCH_;
xor_i4        return (int)Tokens.XOR_I4;
xor_i8        return (int)Tokens.XOR_I8;
xor_i        return (int)Tokens.XOR_I;
sizeof_        return (int)Tokens.SIZEOF_;
malloc        return (int)Tokens.MALLOC;
assign_i4        return (int)Tokens.ASSIGN_I4;
assign_i8        return (int)Tokens.ASSIGN_I8;
assign_r4        return (int)Tokens.ASSIGN_R4;
assign_r8        return (int)Tokens.ASSIGN_R8;
assign_i        return (int)Tokens.ASSIGN_I;
assign_vt        return (int)Tokens.ASSIGN_VT;
assign_v_i4        return (int)Tokens.ASSIGN_V_I4;
assign_v_i8        return (int)Tokens.ASSIGN_V_I8;
assign_v_i        return (int)Tokens.ASSIGN_V_I;
assign_to_virtftnptr        return (int)Tokens.ASSIGN_TO_VIRTFTNPTR;
assign_from_virtftnptr_ptr        return (int)Tokens.ASSIGN_FROM_VIRTFTNPTR_PTR;
assign_from_virtftnptr_thisadjust        return (int)Tokens.ASSIGN_FROM_VIRTFTNPTR_THISADJUST;
assign_virtftnptr        return (int)Tokens.ASSIGN_VIRTFTNPTR;
ldobj_virtftnptr        return (int)Tokens.LDOBJ_VIRTFTNPTR;
label        return (int)Tokens.LABEL;
loc_label        return (int)Tokens.LOC_LABEL;
instruction_label        return (int)Tokens.INSTRUCTION_LABEL;
enter        return (int)Tokens.ENTER;
nop        return (int)Tokens.NOP;
phi_i        return (int)Tokens.PHI_I;
phi_i4        return (int)Tokens.PHI_I4;
phi_i8        return (int)Tokens.PHI_I8;
phi_r4        return (int)Tokens.PHI_R4;
phi_r8        return (int)Tokens.PHI_R8;
phi_vt        return (int)Tokens.PHI_VT;
peek_u1        return (int)Tokens.PEEK_U1;
peek_u2        return (int)Tokens.PEEK_U2;
peek_u4        return (int)Tokens.PEEK_U4;
peek_u8        return (int)Tokens.PEEK_U8;
peek_u        return (int)Tokens.PEEK_U;
peek_i1        return (int)Tokens.PEEK_I1;
peek_i2        return (int)Tokens.PEEK_I2;
peek_r4        return (int)Tokens.PEEK_R4;
peek_r8        return (int)Tokens.PEEK_R8;
poke_u1        return (int)Tokens.POKE_U1;
poke_u2        return (int)Tokens.POKE_U2;
poke_u4        return (int)Tokens.POKE_U4;
poke_u8        return (int)Tokens.POKE_U8;
poke_u        return (int)Tokens.POKE_U;
poke_r4        return (int)Tokens.POKE_R4;
poke_r8        return (int)Tokens.POKE_R8;
portout_u2_u1        return (int)Tokens.PORTOUT_U2_U1;
portout_u2_u2        return (int)Tokens.PORTOUT_U2_U2;
portout_u2_u4        return (int)Tokens.PORTOUT_U2_U4;
portout_u2_u8        return (int)Tokens.PORTOUT_U2_U8;
portout_u2_u        return (int)Tokens.PORTOUT_U2_U;
portin_u2_u1        return (int)Tokens.PORTIN_U2_U1;
portin_u2_u2        return (int)Tokens.PORTIN_U2_U2;
portin_u2_u4        return (int)Tokens.PORTIN_U2_U4;
portin_u2_u8        return (int)Tokens.PORTIN_U2_U8;
portin_u2_u        return (int)Tokens.PORTIN_U2_U;
try_acquire_i8        return (int)Tokens.TRY_ACQUIRE_I8;
release_i8        return (int)Tokens.RELEASE_I8;
sqrt_r8        return (int)Tokens.SQRT_R8;
alloca_i4        return (int)Tokens.ALLOCA_I4;
alloca_i        return (int)Tokens.ALLOCA_I;
zeromem        return (int)Tokens.ZEROMEM;
ldcatchobj        return (int)Tokens.LDCATCHOBJ;
ldmethinfo        return (int)Tokens.LDMETHINFO;
endfinally        return (int)Tokens.ENDFINALLY;
localarg        return (int)Tokens.LOCALARG;
beq_i4        return (int)Tokens.BEQ_I4;
beq_i8        return (int)Tokens.BEQ_I8;
beq_i        return (int)Tokens.BEQ_I;
beq_r8        return (int)Tokens.BEQ_R8;
beq_r4        return (int)Tokens.BEQ_R4;
beq_r8_un        return (int)Tokens.BEQ_R8_UN;
beq_r4_un        return (int)Tokens.BEQ_R4_UN;
bne_i4        return (int)Tokens.BNE_I4;
bne_i8        return (int)Tokens.BNE_I8;
bne_i        return (int)Tokens.BNE_I;
bne_r8        return (int)Tokens.BNE_R8;
bne_r4        return (int)Tokens.BNE_R4;
bne_r8_un        return (int)Tokens.BNE_R8_UN;
bne_r4_un        return (int)Tokens.BNE_R4_UN;
bg_i4        return (int)Tokens.BG_I4;
bg_i8        return (int)Tokens.BG_I8;
bg_i        return (int)Tokens.BG_I;
bg_r8        return (int)Tokens.BG_R8;
bg_r4        return (int)Tokens.BG_R4;
bg_r8_un        return (int)Tokens.BG_R8_UN;
bg_r4_un        return (int)Tokens.BG_R4_UN;
bge_i4        return (int)Tokens.BGE_I4;
bge_i8        return (int)Tokens.BGE_I8;
bge_i        return (int)Tokens.BGE_I;
bge_r8        return (int)Tokens.BGE_R8;
bge_r4        return (int)Tokens.BGE_R4;
bge_r8_un        return (int)Tokens.BGE_R8_UN;
bge_r4_un        return (int)Tokens.BGE_R4_UN;
bl_i4        return (int)Tokens.BL_I4;
bl_i8        return (int)Tokens.BL_I8;
bl_i        return (int)Tokens.BL_I;
bl_r8        return (int)Tokens.BL_R8;
bl_r4        return (int)Tokens.BL_R4;
bl_r8_un        return (int)Tokens.BL_R8_UN;
bl_r4_un        return (int)Tokens.BL_R4_UN;
ble_i4        return (int)Tokens.BLE_I4;
ble_i8        return (int)Tokens.BLE_I8;
ble_i        return (int)Tokens.BLE_I;
ble_r8        return (int)Tokens.BLE_R8;
ble_r4        return (int)Tokens.BLE_R4;
ble_r8_un        return (int)Tokens.BLE_R8_UN;
ble_r4_un        return (int)Tokens.BLE_R4_UN;
ba_i4        return (int)Tokens.BA_I4;
ba_i8        return (int)Tokens.BA_I8;
ba_i        return (int)Tokens.BA_I;
ba_r8        return (int)Tokens.BA_R8;
ba_r4        return (int)Tokens.BA_R4;
ba_r8_un        return (int)Tokens.BA_R8_UN;
ba_r4_un        return (int)Tokens.BA_R4_UN;
bae_i4        return (int)Tokens.BAE_I4;
bae_i8        return (int)Tokens.BAE_I8;
bae_i        return (int)Tokens.BAE_I;
bae_r8        return (int)Tokens.BAE_R8;
bae_r4        return (int)Tokens.BAE_R4;
bae_r8_un        return (int)Tokens.BAE_R8_UN;
bae_r4_un        return (int)Tokens.BAE_R4_UN;
bb_i4        return (int)Tokens.BB_I4;
bb_i8        return (int)Tokens.BB_I8;
bb_i        return (int)Tokens.BB_I;
bb_r8        return (int)Tokens.BB_R8;
bb_r4        return (int)Tokens.BB_R4;
bb_r8_un        return (int)Tokens.BB_R8_UN;
bb_r4_un        return (int)Tokens.BB_R4_UN;
bbe_i4        return (int)Tokens.BBE_I4;
bbe_i8        return (int)Tokens.BBE_I8;
bbe_i        return (int)Tokens.BBE_I;
bbe_r8        return (int)Tokens.BBE_R8;
bbe_r4        return (int)Tokens.BBE_R4;
bbe_r8_un        return (int)Tokens.BBE_R8_UN;
bbe_r4_un        return (int)Tokens.BBE_R4_UN;
misc        return (int)Tokens.MISC;

[L][0-9]+\:					yylval.intval = Int32.Parse(yytext.Substring(1, yytext.Length - 2)); return (int)Tokens.INTLABEL;
[a-zA-Z][a-zA-Z0-9_\#`]*		yylval.strval = yytext; return (int)Tokens.STRING;

