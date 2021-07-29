COMPILER = gcc -std=c17 -O3 -pthread
LIBNAME = MySemaphore
TARGET = lib$(LIBNAME).a
OBJECTS = MySemaphore.o
DEPENDENCIES = $(patsubst %.o,%.d,$(OBJECTS))

$(TARGET): $(OBJECTS)
	ar rcs $@ $^

$(OBJECTS): %.o: %.c
	$(COMPILER) -o $@ -c $<

$(DEPENDENCIES): %.d: %.c
	$(COMPILER) -o $@ -MM $<

include $(DEPENDENCIES)

.PHONY: clean
clean:
	rm $(OBJECTS) $(DEPENDENCIES) $(TARGET)
