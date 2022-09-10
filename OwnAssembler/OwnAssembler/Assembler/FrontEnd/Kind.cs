namespace OwnAssembler.Assembler.FrontEnd;

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
    Mod,
    
    
    Int,
    String,
    Double,
    Char,


    Eof,
    Whitespace,
    Unknown,
    NewLine,
    ReadLine,
    
    
    Define
}