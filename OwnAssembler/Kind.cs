namespace OwnAssembler;

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

    GetTimeInMilliseconds,

    Exit,


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