# Content of demodata

This is a description of the content of `datablocks.json` which you can use to start a preset S7 server.

All mentioned byte indices are 0-based!

1. The first 16 bytes are a ASCII encoded String `Hello, S7!` with trailing spaces.
2. byte 16/17 are the max/min value of a signed byte (127/-128)
3. bytes 32-33/34-35 are the max/min value of a signed short
4. bytes 48-51/52-55 are the max/min value of a signed integer
5. bytes 64-71/72-79 are the max/min value of a signed long
6. bytes 80-83/84-87 are the max/min value of a float
7. bytes 96-103/104-111 are the max/min value of a double
8. bytes 1024-2048 are just bytes starting from 0 continousely incremented