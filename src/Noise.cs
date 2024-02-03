#nullable disable
namespace ProtoDisplayDriver
{
  public class Noise
  {
    private static readonly byte[] Perm = 
    {
      151,
      160,
      137,
      91,
      90,
      15,
      131,
      13,
      201,
      95,
      96,
      53,
      194,
      233,
      7,
      225,
      140,
      36,
      103,
      30,
      69,
      142,
      8,
      99,
      37,
      240,
      21,
      10,
      23,
      190,
      6,
      148,
      247,
      120,
      234,
      75,
      0,
      26,
      197,
      62,
      94,
      252,
      219,
      203,
      117,
      35,
      11,
      32,
      57,
      177,
      33,
      88,
      237,
      149,
      56,
      87,
      174,
      20,
      125,
      136,
      171,
      168,
      68,
      175,
      74,
      165,
      71,
      134,
      139,
      48,
      27,
      166,
      77,
      146,
      158,
      231,
      83,
      111,
      229,
      122,
      60,
      211,
      133,
      230,
      220,
      105,
      92,
      41,
      55,
      46,
      245,
      40,
      244,
      102,
      143,
      54,
      65,
      25,
      63,
      161,
      1,
      216,
      80,
      73,
      209,
      76,
      132,
      187,
      208,
      89,
      18,
      169,
      200,
      196,
      135,
      130,
      116,
      188,
      159,
      86,
      164,
      100,
      109,
      198,
      173,
      186,
      3,
      64,
      52,
      217,
      226,
      250,
      124,
      123,
      5,
      202,
      38,
      147,
      118,
      126,
      byte.MaxValue,
      82,
      85,
      212,
      207,
      206,
      59,
      227,
      47,
      16,
      58,
      17,
      182,
      189,
      28,
      42,
      223,
      183,
      170,
      213,
      119,
      248,
      152,
      2,
      44,
      154,
      163,
      70,
      221,
      153,
      101,
      155,
      167,
      43,
      172,
      9,
      129,
      22,
      39,
      253,
      19,
      98,
      108,
      110,
      79,
      113,
      224,
      232,
      178,
      185,
      112,
      104,
      218,
      246,
      97,
      228,
      251,
      34,
      242,
      193,
      238,
      210,
      144,
      12,
      191,
      179,
      162,
      241,
      81,
      51,
      145,
      235,
      249,
      14,
      239,
      107,
      49,
      192,
      214,
      31,
      181,
      199,
      106,
      157,
      184,
      84,
      204,
      176,
      115,
      121,
      50,
      45,
      127,
      4,
      150,
      254,
      138,
      236,
      205,
      93,
      222,
      114,
      67,
      29,
      24,
      72,
      243,
      141,
      128,
      195,
      78,
      66,
      215,
      61,
      156,
      180,
      151,
      160,
      137,
      91,
      90,
      15,
      131,
      13,
      201,
      95,
      96,
      53,
      194,
      233,
      7,
      225,
      140,
      36,
      103,
      30,
      69,
      142,
      8,
      99,
      37,
      240,
      21,
      10,
      23,
      190,
      6,
      148,
      247,
      120,
      234,
      75,
      0,
      26,
      197,
      62,
      94,
      252,
      219,
      203,
      117,
      35,
      11,
      32,
      57,
      177,
      33,
      88,
      237,
      149,
      56,
      87,
      174,
      20,
      125,
      136,
      171,
      168,
      68,
      175,
      74,
      165,
      71,
      134,
      139,
      48,
      27,
      166,
      77,
      146,
      158,
      231,
      83,
      111,
      229,
      122,
      60,
      211,
      133,
      230,
      220,
      105,
      92,
      41,
      55,
      46,
      245,
      40,
      244,
      102,
      143,
      54,
      65,
      25,
      63,
      161,
      1,
      216,
      80,
      73,
      209,
      76,
      132,
      187,
      208,
      89,
      18,
      169,
      200,
      196,
      135,
      130,
      116,
      188,
      159,
      86,
      164,
      100,
      109,
      198,
      173,
      186,
      3,
      64,
      52,
      217,
      226,
      250,
      124,
      123,
      5,
      202,
      38,
      147,
      118,
      126,
      byte.MaxValue,
      82,
      85,
      212,
      207,
      206,
      59,
      227,
      47,
      16,
      58,
      17,
      182,
      189,
      28,
      42,
      223,
      183,
      170,
      213,
      119,
      248,
      152,
      2,
      44,
      154,
      163,
      70,
      221,
      153,
      101,
      155,
      167,
      43,
      172,
      9,
      129,
      22,
      39,
      253,
      19,
      98,
      108,
      110,
      79,
      113,
      224,
      232,
      178,
      185,
      112,
      104,
      218,
      246,
      97,
      228,
      251,
      34,
      242,
      193,
      238,
      210,
      144,
      12,
      191,
      179,
      162,
      241,
      81,
      51,
      145,
      235,
      249,
      14,
      239,
      107,
      49,
      192,
      214,
      31,
      181,
      199,
      106,
      157,
      184,
      84,
      204,
      176,
      115,
      121,
      50,
      45,
      127,
      4,
      150,
      254,
      138,
      236,
      205,
      93,
      222,
      114,
      67,
      29,
      24,
      72,
      243,
      141,
      128,
      195,
      78,
      66,
      215,
      61,
      156,
      180
    };

    public static float Generate(float x)
    {
      int num1 = FastFloor(x);
      int num2 = num1 + 1;
      float x1 = x - num1;
      float x2 = x1 - 1f;
      double num3 = 1.0 - x1 * (double) x1;
      double num4 = num3 * num3;
      float num5 = (float) (num4 * num4) * Grad(Perm[num1 & byte.MaxValue], x1);
      double num6 = 1.0 - x2 * (double) x2;
      double num7 = num6 * num6;
      float num8 = (float) (num7 * num7) * Grad(Perm[num2 & byte.MaxValue], x2);
      return (float) (0.39500001072883606 * (num5 + (double) num8));
    }

    public static float Generate(float x, float y)
    {
      float num1 = (float) ((x + (double) y) * 0.3660253882408142);
      double x1 = x + (double) num1;
      float x2 = y + num1;
      int num2 = FastFloor((float) x1);
      int num3 = FastFloor(x2);
      float num4 = (num2 + num3) * 0.21132487f;
      float num5 = num2 - num4;
      float num6 = num3 - num4;
      float x3 = x - num5;
      float y1 = y - num6;
      int num7;
      int num8;
      if (x3 > (double) y1)
      {
        num7 = 1;
        num8 = 0;
      }
      else
      {
        num7 = 0;
        num8 = 1;
      }
      float x4 = (float) (x3 - (double) num7 + 0.21132487058639526);
      float y2 = (float) (y1 - (double) num8 + 0.21132487058639526);
      float x5 = (float) (x3 - 1.0 + 0.4226497411727905);
      float y3 = (float) (y1 - 1.0 + 0.4226497411727905);
      int num9 = num2 % 256;
      int index = num3 % 256;
      float num10 = (float) (0.5 - x3 * (double) x3 - y1 * (double) y1);
      float num11;
      if (num10 < 0.0)
      {
        num11 = 0.0f;
      }
      else
      {
        float num12 = num10 * num10;
        num11 = num12 * num12 * Grad(Perm[num9 + Perm[index]], x3, y1);
      }
      float num13 = (float) (0.5 - x4 * (double) x4 - y2 * (double) y2);
      float num14;
      if (num13 < 0.0)
      {
        num14 = 0.0f;
      }
      else
      {
        float num15 = num13 * num13;
        num14 = num15 * num15 * Grad(Perm[num9 + num7 + Perm[index + num8]], x4, y2);
      }
      float num16 = (float) (0.5 - x5 * (double) x5 - y3 * (double) y3);
      float num17;
      if (num16 < 0.0)
      {
        num17 = 0.0f;
      }
      else
      {
        float num18 = num16 * num16;
        num17 = num18 * num18 * Grad(Perm[num9 + 1 + Perm[index + 1]], x5, y3);
      }
      return (float) (40.0 * (num11 + (double) num14 + num17));
    }

    public static float Generate(float x, float y, float z)
    {
      float num1 = (float) ((x + (double) y + z) * 0.3333333432674408);
      double x1 = x + (double) num1;
      float x2 = y + num1;
      float x3 = z + num1;
      int x4 = FastFloor((float) x1);
      int x5 = FastFloor(x2);
      int x6 = FastFloor(x3);
      float num2 = (x4 + x5 + x6) * 0.16666667f;
      float num3 = x4 - num2;
      float num4 = x5 - num2;
      float num5 = x6 - num2;
      float x7 = x - num3;
      float y1 = y - num4;
      float z1 = z - num5;
      int num6;
      int num7;
      int num8;
      int num9;
      int num10;
      int num11;
      if (x7 >= (double) y1)
      {
        if (y1 >= (double) z1)
        {
          num6 = 1;
          num7 = 0;
          num8 = 0;
          num9 = 1;
          num10 = 1;
          num11 = 0;
        }
        else if (x7 >= (double) z1)
        {
          num6 = 1;
          num7 = 0;
          num8 = 0;
          num9 = 1;
          num10 = 0;
          num11 = 1;
        }
        else
        {
          num6 = 0;
          num7 = 0;
          num8 = 1;
          num9 = 1;
          num10 = 0;
          num11 = 1;
        }
      }
      else if (y1 < (double) z1)
      {
        num6 = 0;
        num7 = 0;
        num8 = 1;
        num9 = 0;
        num10 = 1;
        num11 = 1;
      }
      else if (x7 < (double) z1)
      {
        num6 = 0;
        num7 = 1;
        num8 = 0;
        num9 = 0;
        num10 = 1;
        num11 = 1;
      }
      else
      {
        num6 = 0;
        num7 = 1;
        num8 = 0;
        num9 = 1;
        num10 = 1;
        num11 = 0;
      }
      float x8 = (float) (x7 - (double) num6 + 0.1666666716337204);
      float y2 = (float) (y1 - (double) num7 + 0.1666666716337204);
      float z2 = (float) (z1 - (double) num8 + 0.1666666716337204);
      float x9 = (float) (x7 - (double) num9 + 0.3333333432674408);
      float y3 = (float) (y1 - (double) num10 + 0.3333333432674408);
      float z3 = (float) (z1 - (double) num11 + 0.3333333432674408);
      float x10 = (float) (x7 - 1.0 + 0.5);
      float y4 = (float) (y1 - 1.0 + 0.5);
      float z4 = (float) (z1 - 1.0 + 0.5);
      int num12 = Mod(x4, 256);
      int num13 = Mod(x5, 256);
      int index = Mod(x6, 256);
      float num14 = (float) (0.6000000238418579 - x7 * (double) x7 - y1 * (double) y1 - z1 * (double) z1);
      float num15;
      if (num14 < 0.0)
      {
        num15 = 0.0f;
      }
      else
      {
        float num16 = num14 * num14;
        num15 = num16 * num16 * Grad(Perm[num12 + Perm[num13 + Perm[index]]], x7, y1, z1);
      }
      float num17 = (float) (0.6000000238418579 - x8 * (double) x8 - y2 * (double) y2 - z2 * (double) z2);
      float num18;
      if (num17 < 0.0)
      {
        num18 = 0.0f;
      }
      else
      {
        float num19 = num17 * num17;
        num18 = num19 * num19 * Grad(Perm[num12 + num6 + Perm[num13 + num7 + Perm[index + num8]]], x8, y2, z2);
      }
      float num20 = (float) (0.6000000238418579 - x9 * (double) x9 - y3 * (double) y3 - z3 * (double) z3);
      float num21;
      if (num20 < 0.0)
      {
        num21 = 0.0f;
      }
      else
      {
        float num22 = num20 * num20;
        num21 = num22 * num22 * Grad(Perm[num12 + num9 + Perm[num13 + num10 + Perm[index + num11]]], x9, y3, z3);
      }
      float num23 = (float) (0.6000000238418579 - x10 * (double) x10 - y4 * (double) y4 - z4 * (double) z4);
      float num24;
      if (num23 < 0.0)
      {
        num24 = 0.0f;
      }
      else
      {
        float num25 = num23 * num23;
        num24 = num25 * num25 * Grad(Perm[num12 + 1 + Perm[num13 + 1 + Perm[index + 1]]], x10, y4, z4);
      }
      return (float) (32.0 * (num15 + (double) num18 + num21 + num24));
    }

    private static int FastFloor(float x) => x <= 0.0 ? (int) x - 1 : (int) x;

    private static int Mod(int x, int m)
    {
      int num = x % m;
      return num >= 0 ? num : num + m;
    }

    private static float Grad(int hash, float x)
    {
      int num1 = hash & 15;
      float num2 = 1f + (num1 & 7);
      if ((num1 & 8) != 0)
        num2 = -num2;
      return num2 * x;
    }

    private static float Grad(int hash, float x, float y)
    {
      int num1 = hash & 7;
      float num2 = num1 < 4 ? x : y;
      float num3 = num1 < 4 ? y : x;
      return (float) (((num1 & 1) != 0 ? -(double) num2 : num2) + ((num1 & 2) != 0 ? -2.0 * num3 : 2.0 * num3));
    }

    private static float Grad(int hash, float x, float y, float z)
    {
      int num1 = hash & 15;
      float num2 = num1 < 8 ? x : y;
      float num3 = num1 < 4 ? y : (num1 == 12 || num1 == 14 ? x : z);
      return (float) (((num1 & 1) != 0 ? -(double) num2 : num2) + ((num1 & 2) != 0 ? -(double) num3 : num3));
    }

    private static float Grad(int hash, float x, float y, float z, float t)
    {
      int num1 = hash & 31;
      float num2 = num1 < 24 ? x : y;
      float num3 = num1 < 16 ? y : z;
      float num4 = num1 < 8 ? z : t;
      return (float) (((num1 & 1) != 0 ? -(double) num2 : num2) + ((num1 & 2) != 0 ? -(double) num3 : num3) + ((num1 & 4) != 0 ? -(double) num4 : num4));
    }
  }
}
