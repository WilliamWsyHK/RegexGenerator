using System;
using System.Collections.Generic;
using System.Text;

namespace WilliamWsy.RegexGenerator
{
    /// <summary>
    /// Flag for unicode category used in regex.
    /// </summary>
    /// <remarks>http://www.unicode.org/reports/tr44/#General_Category_Values</remarks>
    [Flags]
    public enum RegexUnicodeCategoryFlag
    {
        /// <summary>
        /// An uppercase letter.
        /// </summary>
        Lu = 0b00_0000_0000_0000_0000_0000_0000_0001,
        /// <summary>
        /// A lowercase letter.
        /// </summary>
        Ll = 0b00_0000_0000_0000_0000_0000_0000_0010,
        /// <summary>
        /// A digraphic character, with first part uppercase.
        /// </summary>
        Lt = 0b00_0000_0000_0000_0000_0000_0000_0100,
        LC = Lu | Ll | Lt,
        /// <summary>
        /// A modifier letter.
        /// </summary>
        Lm = 0b00_0000_0000_0000_0000_0000_0000_1000,
        /// <summary>
        /// Other letters, including syllables and ideographs.
        /// </summary>
        Lo = 0b00_0000_0000_0000_0000_0000_0001_0000,
        L  = Lu | Ll | Lt | Lm | Lo,

        /// <summary>
        /// A nonspacing combining mark (zero advance width).
        /// </summary>
        Mn = 0b00_0000_0000_0000_0000_0000_0010_0000,
        /// <summary>
        /// A spacing combining mark (positive advance width).
        /// </summary>
        Mc = 0b00_0000_0000_0000_0000_0000_0100_0000,
        /// <summary>
        /// An enclosing combining mark.
        /// </summary>
        Me = 0b00_0000_0000_0000_0000_0000_1000_0000,
        M  = Mn | Mc | Me,

        /// <summary>
        /// A decimal digit.
        /// </summary>
        Nd = 0b00_0000_0000_0000_0000_0001_0000_0000,
        /// <summary>
        /// A letterlike numeric character.
        /// </summary>
        Nl = 0b00_0000_0000_0000_0000_0010_0000_0000,
        /// <summary>
        /// A numeric character of other type.
        /// </summary>
        No = 0b00_0000_0000_0000_0000_0100_0000_0000,
        N  = Nd | Nl | No,

        /// <summary>
        /// A connecting punctuation mark, like a tie.
        /// </summary>
        Pc = 0b00_0000_0000_0000_0000_1000_0000_0000,
        /// <summary>
        /// A dash or hyphen punctuation mark.
        /// </summary>
        Pd = 0b00_0000_0000_0000_0001_0000_0000_0000,
        /// <summary>
        /// An opening punctuation mark (of a pair).
        /// </summary>
        Ps = 0b00_0000_0000_0000_0010_0000_0000_0000,
        /// <summary>
        ///  closing punctuation mark (of a pair).
        /// </summary>
        Pe = 0b00_0000_0000_0000_0100_0000_0000_0000,
        /// <summary>
        /// An initial quotation mark.
        /// </summary>
        Pi = 0b00_0000_0000_0000_1000_0000_0000_0000,
        /// <summary>
        /// A final quotation mark.
        /// </summary>
        Pf = 0b00_0000_0000_0001_0000_0000_0000_0000,
        /// <summary>
        /// A punctuation mark of other type.
        /// </summary>
        Po = 0b00_0000_0000_0010_0000_0000_0000_0000,
        P  = Pc | Pd | Ps | Pe | Pi | Pf | Po,

        /// <summary>
        /// A symbol of mathematical use.
        /// </summary>
        Sm = 0b00_0000_0000_0100_0000_0000_0000_0000,
        /// <summary>
        /// A currency sign.
        /// </summary>
        Sc = 0b00_0000_0000_1000_0000_0000_0000_0000,
        /// <summary>
        /// A non-letterlike modifier symbol.
        /// </summary>
        Sk = 0b00_0000_0001_0000_0000_0000_0000_0000,
        /// <summary>
        /// A symbol of other type.
        /// </summary>
        So = 0b00_0000_0010_0000_0000_0000_0000_0000,
        S  = Sm | Sc | Sk | So,

        /// <summary>
        /// A space character (of various non-zero widths).
        /// </summary>
        Zs = 0b00_0000_0100_0000_0000_0000_0000_0000,
        /// <summary>
        /// U+2028 LINE SEPARATOR only.
        /// </summary>
        Zl = 0b00_0000_1000_0000_0000_0000_0000_0000,
        /// <summary>
        /// U+2029 PARAGRAPH SEPARATOR only.
        /// </summary>
        Zp = 0b00_0001_0000_0000_0000_0000_0000_0000,
        Z  = Zs | Zl | Zp,

        /// <summary>
        /// A C0 or C1 control code.
        /// </summary>
        Cc = 0b00_0010_0000_0000_0000_0000_0000_0000,
        /// <summary>
        /// A format control character.
        /// </summary>
        Cf = 0b00_0100_0000_0000_0000_0000_0000_0000,
        /// <summary>
        /// A surrogate code point.
        /// </summary>
        Cs = 0b00_1000_0000_0000_0000_0000_0000_0000,
        /// <summary>
        /// A private-use character.
        /// </summary>
        Co = 0b01_0000_0000_0000_0000_0000_0000_0000,
        /// <summary>
        /// A reserved unassigned code point or a noncharacter.
        /// </summary>
        Cn = 0b10_0000_0000_0000_0000_0000_0000_0000,
        C  = Cc | Cf | Cs | Co | Cn
    }
}
