namespace OwnAssembler.Assembler;

public enum Kind
{
    Add,
    Equals,
    Gt,
    Lt,
    Sub,
    Jmp,
    Output,
    ReadKey,
    Clear,
    GetTimeInMilliseconds,
    Copy,
    SetPriority,
    Nop,
    Exit,

    Push,
    Pop,

    If,
    Else,
    EndIf,

    RamRead,
    RamWrite,

    Goto,
    SetMark,

    Call,
    Ret,

    Invoke,
    Import,


    Int,


    Eof,
    Whitespace,
    Unknown,
    NewLine,
    ReadLine,
    Char
}