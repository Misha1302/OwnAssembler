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

    While,
    EndWhile,

    Call,
    Ret,

    ConvertToDouble,
    ConvertToInt,
    ConvertToString,
    ConvertToBool,
    ConvertToChar,

    Invoke,
    Import,


    Int,
    String,
    Double,


    Eof,
    Whitespace,
    Unknown,
    NewLine,
    ReadLine,
    Char
}