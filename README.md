



# OwnAssembler
A pseudo programming language similar to assembler

---
О решении (solution):
- Проект написан на версии CSharp 10
- Платформа net core

---
Аргументы ассемблера:
- -codePath 	ПУТЬ
- -compile 		true/false
- -byteCodeRead ПУТЬ
- -bytecodesave ПУТЬ
- -debug 		true/false

Аргументы можно писать в любом регистре, но стоит соблюдать регистр пути файла

---
Проекты:
- Cpu - отвечает за запуск и синхронизацию работы приложений
- OwnAssembler - отвечает за компиляцию ассемблерного кода в байт-код, за сохранение и чтение байт-кода. Запускает Cpu и передает байт-код, как первое приложение 
- Connector - отвечает за соединение проектов: OwnAssembler и Cpu

---
Алгоритм написания библиотек для ассемблера (Пример библиотеки по пути:- "Examples\ImportDll\AssemblerDll"):

0. Создать проект с библиотекой
1. Подключить сборку ассемблера
2. Установить namespace AssemblerDllNamespace (в нем будет содержаться класс AssemblerDllClass)
3. Создать статический класс AssemblerDllClass (из этого класса можно будет вызывать методы из ассемблера)
4. Создать статические методы, которые вы будете использовать в ассемблере
5. Все методы, что будут использоваться в ассемблере, должны принимать параметры (важно соблюдать порядок параметров): 
- CpuStack stack
- List\<ICommand\> commands
- RefCurrentCommandIndex currentCommandIndex
6. Возможно, Вам понадобятся using'и (совет):
- using OwnAssembler.Assembler.HighLevelCommands;
- using OwnAssembler.Assembler.LowLevelCommands;
- using OwnAssembler.Assembler.LowLevelCommands.Dlls;
- using OwnAssembler.CentralProcessingUnit;

---
Все команды доступные на версии v0.4 (регистр команд не важен):
- add
- equals
- gt
- lt
- sub
- jmp
- clear
- getTimeMs
- copy
- nop
- exit
- toStr
- toInt
- toDouble
- toChar
- output
- readKey
- readline
- and
- or
- not
- xor
- dev
- mul
- shr
- shl
- mod
- push
- pop
- if
- else
- endif
- ramRead
- ramWrite
- setMark
- goto
- call
- ret
- import
- invoke
- #define

---
Комментарии:
- однострочный комментарий - комментарий начинается с символа ';' и продолжается до конца строки

---
Debug mode:
- чтобы включить debug mode, нужно запустить ассемблер с аргументом:  "-debug true"
- логи стека хранятся в файле "stackLog.txt", в директории с программой
- логи RAM хранятся в файле "stackLog.txt", в директории с программой
- Хранение логов:
	 - Пример: `5   | 0: Hello`
	 - 5 - номер команды
	 - 0 - номер регистра
	 - "Hello" - значение в регистре

---
О синтаксисе:
- в проекте есть файл "AsmEasyHighlight.xml"
- если импортировать его в notepad++, то вы получите подсветку синтаксиса
- возможно, вам понравится сочетание подсветки с этими темами:
  - vibrant ink
  - bespin (brown - chocolate)
  - black board (dark blue - black)
  - choco (chocolate)
  - DansLeRuSH-Dark (grey)
  - HotFudgeDundae (red - chocolate)
  - Mono Industrial (blue-green-grey)
  - Obsidian (blue - grey)
  - Solarized (blue - grey, and Consolas font)
  - Twilight (black, and Consolas font)
  - vim Dark Blue (blue, and Unknown font)
---

---
Мои контакты:
telegram - @Micodiy
vk - [Миша Разаков (vk.com)](https://vk.com/misha13022008)
discord - Разаков Миша#7375

---
**Readme написан под версию v0.4**
