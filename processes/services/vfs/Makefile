TYBUILD=../../../tybuild/bin/Release/tybuild.exe
TYSILA=../../../tysila2/bin/Release/tysila2.exe
MSCORLIB=../../../mono/corlib/mscorlib.obj
LD=x86_64-elf-ld

TYSILAFLAGS=-L../../../mono/corlib -L../../../tysos/bin/Release --whole-module --exclude $(MSCORLIB_SYMS) --exclude $(TYSOS_SYMS) -q
LDFLAGS=-shared

PEFILE=bin/Release/vfs.exe
OBJFILE=vfs.obj
OUTPUT=vfs.bin

MSCORLIB_SYMS=../../../mono/corlib/mscorlib_syms.txt
TYSOS_SYMS=../../../tysos/tysos_syms.txt
VFS_SYMS=vfs_syms.txt

.PHONY: clean $(PEFILE) $(TYSOS_SYMS)

all: $(OUTPUT)

$(TYSILA):
	cd ../../../tysila2 && make

$(MSCORLIB):
	cd ../../../mono/corlib && make

$(MSCORLIB_SYMS):
	cd ../../../mono/corlib && make mscorlib_syms.txt

$(TYSOS_SYMS):
	cd ../../../tysos && make tysos_syms.txt

$(VFS_SYMS): $(OUTPUT)
	./make_vfs_syms.sh

$(OUTPUT): $(OBJFILE)
	$(LD) $(LDFLAGS) -o $(OUTPUT) $(OBJFILE)

$(OBJFILE): $(PEFILE) $(TYSILA) $(MSCORLIB_SYMS) $(TYSOS_SYMS)
	$(TYSILA) $(TYSILAFLAGS) -o $(OBJFILE) $(PEFILE)

$(PEFILE): $(TYBUILD)
	$(TYBUILD) /p:Configuration=Release /v /tools:3_5

clean:
	rm -rf obj bin $(PEFILE) $(OBJFILE) $(OUTPUT)

