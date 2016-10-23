################################################################################
# Automatically-generated file. Do not edit!
################################################################################

# Add inputs and outputs from these tool invocations to the build variables 
C_SRCS += \
../src/List.c \
../src/Shell2.c \
../src/Start2.c \
../src/getPath.c 

OBJS += \
./src/List.o \
./src/Shell2.o \
./src/Start2.o \
./src/getPath.o 

C_DEPS += \
./src/List.d \
./src/Shell2.d \
./src/Start2.d \
./src/getPath.d 


# Each subdirectory must supply rules for building sources it contributes
src/%.o: ../src/%.c
	@echo 'Building file: $<'
	@echo 'Invoking: Cygwin C Compiler'
	gcc -O0 -g3 -Wall -c -fmessage-length=0 -MMD -MP -MF"$(@:%.o=%.d)" -MT"$(@:%.o=%.d)" -o"$@" "$<"
	@echo 'Finished building: $<'
	@echo ' '


