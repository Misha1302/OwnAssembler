namespace OwnAssembler.Assembler.Tokens;

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

    ConvertToDouble,
    ConvertToInt,
    ConvertToString,
    ConvertToChar,

    Invoke,
    Import,
    
    And,
    Or,
    Not,
    Xor,
    Division,
    Multiplication,
    ShiftRight,
    ShiftLeft,
    
    
    Int,
    String,
    Double,
    Char,


    Eof,
    Whitespace,
    Unknown,
    NewLine,
    ReadLine,
}