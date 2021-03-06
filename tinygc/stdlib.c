#include <stddef.h>

char *getenv(const char *name)
{
	(void)name;
	return NULL;
}

/* atol() is only called by tinygc if getenv returns NULL */
long atol(const char *str)
{
	(void)str;
	return 0;
}

__attribute__ ((weak))
void *memcpy(void *dest, const void *src, size_t n)
{
#ifdef DEBUG_MEMCPY
	printf("memcpy(%llX, %llX, %X)\n", dest, src, n);
#endif
	char *d = (char *)dest;
	char *s = (char *)src;
	while(n--)
		*d++ = *s++;
	return dest;
}

__attribute__ ((weak))
void *memmove(void *dest, const void *src, size_t n)
{
	char *d = (char *)dest;
	char *s = (char *)src;

	if(dest > src)
	{
		/* Perform a backwards copy */
		d = &d[n];
		s = &s[n];

		while(n--)
			*--d = *--s;
	}
	else
	{
		/* Normal memcpy like copy */
		while(n--)
			*d++ = *s++;
	}
	return dest;
}

__attribute__ ((weak))
void *memset(void *dest, int c, size_t n)
{
	char *d = (char *)dest;
	while(n--)
		*d++ = (char)c;
	return dest;
}

