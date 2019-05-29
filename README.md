# LambMidiLinux
A VERY bare bones Unity3d midi tool for Linux computers using Alsa and AlsaSharp [ABANDONED FOR NOW]

## Why is it abandoned?
Currently this project is abandoned to the limitations of Marshalling/Pinvoke when trying to send a class from c++ to c#. Usually you could use an intptr and call methods from the pointer but an important method in the alsa library does not like intptr being a parameter when sent from c# to c++. Unless this gets fixed, this project will be forever abandoned.
