#Gemini Compiler Project

This project was meant to be an emulator of the working of a processor, built entirely in C#

#### Directory Structure

* [Project Gemini](https://github.com/1abhisheknigam/Projects/tree/master/CSharp/GeminiCompiler/ProjectGemini): Source Code files

* [Screen Captures](https://github.com/1abhisheknigam/Projects/tree/master/CSharp/GeminiCompiler/Screen%20Captures): Some images of the program in action

* [Source Files](https://github.com/1abhisheknigam/Projects/tree/master/CSharp/GeminiCompiler/Source%20Files): These files are written in the custom "Gemini Assembly" language, used as input source code for the emulator

#### Functionality
* The software will take a file name of a file that contains "Gemini Assembly", a brand new language created for the purpose of this class.
* Instruction Set for the new language:
      (Note that $val indicates a memory location
                   #$val indicates an actual value from the text segment)

        - LDA #$val Sets the accumulator with the value
        - LDA $m	Sets the accumulator from a memory location
        - STA $m  Store the accumulator to a memory location
        - ADD $m	Add the value in memory to the accumulator
          ADD #$val     Add the value to the accumulator
        - SUB $m	Subtract the value in memory to the accumulator
          SUB #$val     Subtract the value to the accumulator
        - MUL $m        Multiply the accumulator by the value in memory
          MUL #$val     Multiply the accumulator by the value 
        - DIV $m        Divide the accumulator by the value in memory
          DIV #$val     Divide the accumulator by the value
        - AND $m	Logical "and" of memory and accumulator
          AND #$val     Logical "and" of value and accumulator
        - OR  $m	Logical "or" of memory and accumulator
          OR  #$val     Logical "or" or value and the accumulator
        - SHL #$val Shift the accumulator by the number of bits to the left
        - NOTA		Logical "not" of accumulator
        - BA lbl        Always branch to label (goto)
        - BE lbl	Branch to label if operation resulted in 0
        - BL lbl        Branch to label if operation resulted in Negative
        - BG lbl        Branch to label if operation resulted in Positive 
        - NOP           No Operation (Implemented by adding Zero to the ACC)
        - HLT           Quit the program (not needed if the last line of the program is the end)
* Single direct-accessing memory mode
* 8 Bit memory address field (256 byte main memory for data)
* The software will run in a WinForms GUI (C# .NET) as a frontend GUI to show registers, stack and the parsing of the new language
* The program will load the strings from the file and parse the strings one at a time.
* If a program is invalid in any way, we should quit and tell the user the line number of the failure.
* Finally, the parser will dump out a binary file representing the ASM code in machine language.

Next, the program will start running in the simulator component of the program.If during program execution the simulator notices any problems (namely Segmentation Faults- it should also quit and tell the user the line number of the fault.

When a binaey file is read by the main program the following will happen:
* Display the string in the simulator window to show the user what instruction is currently running. (IR will always show the binary representation of the code, you can also make it display human readable text too).
* Show updates to all registers in the simulator window as the line of code is run.

Furthermore
* Use of cache levels are also simulated and displayed
* Running of commands is done in specific stages e.g. fetch,read and write, mimicking real processors

#### Usage
* The ProjectGemini is pretty simple and easy to use! Hit open to select a file. 
* Files may be reselected at any time As soon as a file is selected, it will automatically compile. 
* If there are sytnax errors it will display in the status. 
* If there is a deeper problem check the Visual Sutdio console. 
* All the registers are updated real time, and you can check a speciific stack element with the "view Stack" button. 
* Hit the "next instruction" button to go to the next instruction, and "run all" to execute all remaining instrucitons. 
* "restart" button will allow you to rerun the program without reselecting the file you want to compile. 
* The current instruction box will show which instruciton is being executed.
