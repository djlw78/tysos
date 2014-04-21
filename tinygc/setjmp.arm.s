/* setjmp implementation - store registers in 'env' array

   int setjmp(jmp_buf env);
*/

.globl setjmp

setjmp:
str r0, [r0, #0]
str r1, [r0, #4]
str r2, [r0, #8]
str r3, [r0, #12]
str r4, [r0, #16]
str r5, [r0, #20]
str r6, [r0, #24]
str r7, [r0, #28]
str r8, [r0, #32]
str r9, [r0, #36]
str r10, [r0, #40]
str r11, [r0, #44]
str r12, [r0, #48]
str r13, [r0, #52]
str r14, [r0, #56]
str r15, [r0, #60]
mov r0, #0
mov pc, lr

