#
#
#
#    Makefile.cs
#
#    Make recipes for building with Mono.
#
#    License: GNU General Public License Version 3.0.
#    
#    Copyright (C) 2014 by Matthew K. Roberts, KK5JY. All rights reserved.
#
#    This program is free software: you can redistribute it and/or modify
#    it under the terms of the GNU General Public License as published by
#    the Free Software Foundation, either version 3 of the License, or
#    (at your option) any later version.
#    
#    This program is distributed in the hope that it will be useful, but
#    WITHOUT ANY WARRANTY; without even the implied warranty of
#    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
#    General Public License for more details.
#    
#    You should have received a copy of the GNU General Public License
#    along with this program.  If not, see: http://www.gnu.org/licenses/
#    
#
#

# the compiler
MCS=mcs

# the debug settings
DEBUG= #-debug+ -define:DEBUG 

# the core files
FILES=\
	Helpers.cs \
	Integrator.cs \
	Interfaces.cs \
	Platform.cs \
	SerialPortHelper.cs \
	SocketHelper.cs \
	YaesuRotor.cs
REFS=

# additional dependencies for the GUI version
WINFORMS=\
	MainForm.cs \
	MainForm.Designer.cs \
	RotorDial.cs \
	RotorDial.Designer.cs \
	Properties/AssemblyInfo.cs \
	Properties/Resources.Designer.cs
WINFORM_REFS=\
	-resource:KK5JY.RotorCraft.MainForm.resources \
	-reference:System.Windows.Forms \
	-reference:System.Drawing
# the application icon to show when browsing in Win32
WINICON=AppIcon.ico

# target definitions
TARGETS=RotorCraft.exe

# resource files for the GUI
RESOURCES=KK5JY.RotorCraft.MainForm.resources KK5JY.RotorCraft.resources

all: $(TARGETS)

KK5JY.RotorCraft.resources:
	resgen2 Properties/Resources.resx KK5JY.RotorCraft.resources
KK5JY.RotorCraft.MainForm.resources:
	resgen2 MainForm.resx KK5JY.RotorCraft.MainForm.resources
RotorCraft.exe: $(RESOURCES) $(FILES) $(WINFORMS)
	$(MCS) $(DEBUG) -out:$@ -sdk:4 -target:winexe -win32icon:$(WINICON) $(FILES) $(WINFORMS) $(REFS) $(WINFORM_REFS)
clean:
	rm -f $(TARGETS) *.mdb *.resources
rebuild: clean all

# EOF
