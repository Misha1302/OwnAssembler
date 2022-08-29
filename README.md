# OwnAssembler
A pseudo programming language similar to assembler

---
О решении (solution):
- Проект написан на версии CSharp 10
- Платформа net core
- 35 классов
- 3 проекта (project)

---
Аргументы ассемблера:
- -codePath 	ПУТЬ
- -compile 		true/false
- -byteCodeRead ПУТЬ
- -bytecodesave ПУТЬ
- Аргументы можно писать в любом регистре, но стоит соблюдать регистр, когда указываете путь

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
- List<ICommand> commands
- RefCurrentCommandIndex currentCommandIndex
6. Возможно, Вам понадобятся using'и (совет):
- using OwnAssembler.Assembler.HighLevelCommands;
- using OwnAssembler.Assembler.LowLevelCommands;
- using OwnAssembler.Assembler.LowLevelCommands.Dlls;
- using OwnAssembler.CentralProcessingUnit;

---
Все команды доступные на версии v0.3 (регистр команд не важен):
- "add"
- "equals"
- "gt"
- "lt"
- "sub"
- "jmp"
- "clear"
- "push"
- "pop"
- "output"
- "readkey"
- "readline"
- "if"
- "else"
- "endif"
- "while"
- "endWhile"
- "ramread"
- "ramwrite"
- "setmark"
- "goto"
- "call"
- "ret"
- "gettimems"
- "exit"
- "import"
- "invoke"
- "copy"
- "converttostring"
- "converttoint"
- "converttodouble"
- "converttobool"
- "converttochar"

---
Комментарии:
- однострочный комментарий - комментарий начинается с символа ';' и продолжается до конца строки



---
**Readme.txt написана под версию ассемблера v0.3**
