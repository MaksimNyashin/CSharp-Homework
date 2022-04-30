import sys

def f(s):
	while len(s) > 0 and s[-1] in "\n\a":
		s = s[:-1];
	return s;

sys.stdin.reconfigure(encoding='cp866')
lines = [f(line) for line in sys.stdin][9:]
if lines != []:
	sys.stdout.write("\n\t\t\tERRORS:\n")
	for line in lines:
		sys.stdout.write(f"{line}\n")
	with open("suc.txt", "w") as fo:
		fo.write("0")
else:
	with open("suc.txt", "w") as fo:
		fo.write("1")