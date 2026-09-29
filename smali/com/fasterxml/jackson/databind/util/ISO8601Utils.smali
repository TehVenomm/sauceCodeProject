.class public Lcom/fasterxml/jackson/databind/util/ISO8601Utils;
.super Ljava/lang/Object;
.source "ISO8601Utils.java"


# static fields
.field private static final GMT_ID:Ljava/lang/String; = "GMT"

.field private static final TIMEZONE_GMT:Ljava/util/TimeZone;


# direct methods
.method static constructor <clinit>()V
    .locals 1

    const-string v0, "GMT"

    .line 25
    invoke-static {v0}, Ljava/util/TimeZone;->getTimeZone(Ljava/lang/String;)Ljava/util/TimeZone;

    move-result-object v0

    sput-object v0, Lcom/fasterxml/jackson/databind/util/ISO8601Utils;->TIMEZONE_GMT:Ljava/util/TimeZone;

    return-void
.end method

.method public constructor <init>()V
    .locals 0

    .line 15
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method

.method private static checkOffset(Ljava/lang/String;IC)Z
    .locals 1

    .line 252
    invoke-virtual {p0}, Ljava/lang/String;->length()I

    move-result v0

    if-ge p1, v0, :cond_0

    invoke-virtual {p0, p1}, Ljava/lang/String;->charAt(I)C

    move-result p0

    if-ne p0, p2, :cond_0

    const/4 p0, 0x1

    goto :goto_0

    :cond_0
    const/4 p0, 0x0

    :goto_0
    return p0
.end method

.method public static format(Ljava/util/Date;)Ljava/lang/String;
    .locals 2

    .line 53
    sget-object v0, Lcom/fasterxml/jackson/databind/util/ISO8601Utils;->TIMEZONE_GMT:Ljava/util/TimeZone;

    const/4 v1, 0x0

    invoke-static {p0, v1, v0}, Lcom/fasterxml/jackson/databind/util/ISO8601Utils;->format(Ljava/util/Date;ZLjava/util/TimeZone;)Ljava/lang/String;

    move-result-object p0

    return-object p0
.end method

.method public static format(Ljava/util/Date;Z)Ljava/lang/String;
    .locals 1

    .line 64
    sget-object v0, Lcom/fasterxml/jackson/databind/util/ISO8601Utils;->TIMEZONE_GMT:Ljava/util/TimeZone;

    invoke-static {p0, p1, v0}, Lcom/fasterxml/jackson/databind/util/ISO8601Utils;->format(Ljava/util/Date;ZLjava/util/TimeZone;)Ljava/lang/String;

    move-result-object p0

    return-object p0
.end method

.method public static format(Ljava/util/Date;ZLjava/util/TimeZone;)Ljava/lang/String;
    .locals 5

    .line 76
    new-instance v0, Ljava/util/GregorianCalendar;

    sget-object v1, Ljava/util/Locale;->US:Ljava/util/Locale;

    invoke-direct {v0, p2, v1}, Ljava/util/GregorianCalendar;-><init>(Ljava/util/TimeZone;Ljava/util/Locale;)V

    .line 77
    invoke-virtual {v0, p0}, Ljava/util/Calendar;->setTime(Ljava/util/Date;)V

    const-string p0, "yyyy-MM-ddThh:mm:ss"

    .line 80
    invoke-virtual {p0}, Ljava/lang/String;->length()I

    move-result p0

    if-eqz p1, :cond_0

    const-string v1, ".sss"

    .line 81
    invoke-virtual {v1}, Ljava/lang/String;->length()I

    move-result v1

    goto :goto_0

    :cond_0
    const/4 v1, 0x0

    :goto_0
    add-int/2addr p0, v1

    .line 82
    invoke-virtual {p2}, Ljava/util/TimeZone;->getRawOffset()I

    move-result v1

    if-nez v1, :cond_1

    const-string v1, "Z"

    :goto_1
    invoke-virtual {v1}, Ljava/lang/String;->length()I

    move-result v1

    goto :goto_2

    :cond_1
    const-string v1, "+hh:mm"

    goto :goto_1

    :goto_2
    add-int/2addr p0, v1

    .line 83
    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1, p0}, Ljava/lang/StringBuilder;-><init>(I)V

    const/4 p0, 0x1

    .line 85
    invoke-virtual {v0, p0}, Ljava/util/Calendar;->get(I)I

    move-result v2

    const-string v3, "yyyy"

    invoke-virtual {v3}, Ljava/lang/String;->length()I

    move-result v3

    invoke-static {v1, v2, v3}, Lcom/fasterxml/jackson/databind/util/ISO8601Utils;->padInt(Ljava/lang/StringBuilder;II)V

    const/16 v2, 0x2d

    .line 86
    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    const/4 v3, 0x2

    .line 87
    invoke-virtual {v0, v3}, Ljava/util/Calendar;->get(I)I

    move-result v3

    add-int/2addr v3, p0

    const-string p0, "MM"

    invoke-virtual {p0}, Ljava/lang/String;->length()I

    move-result p0

    invoke-static {v1, v3, p0}, Lcom/fasterxml/jackson/databind/util/ISO8601Utils;->padInt(Ljava/lang/StringBuilder;II)V

    .line 88
    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    const/4 p0, 0x5

    .line 89
    invoke-virtual {v0, p0}, Ljava/util/Calendar;->get(I)I

    move-result p0

    const-string v3, "dd"

    invoke-virtual {v3}, Ljava/lang/String;->length()I

    move-result v3

    invoke-static {v1, p0, v3}, Lcom/fasterxml/jackson/databind/util/ISO8601Utils;->padInt(Ljava/lang/StringBuilder;II)V

    const/16 p0, 0x54

    .line 90
    invoke-virtual {v1, p0}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    const/16 p0, 0xb

    .line 91
    invoke-virtual {v0, p0}, Ljava/util/Calendar;->get(I)I

    move-result p0

    const-string v3, "hh"

    invoke-virtual {v3}, Ljava/lang/String;->length()I

    move-result v3

    invoke-static {v1, p0, v3}, Lcom/fasterxml/jackson/databind/util/ISO8601Utils;->padInt(Ljava/lang/StringBuilder;II)V

    const/16 p0, 0x3a

    .line 92
    invoke-virtual {v1, p0}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    const/16 v3, 0xc

    .line 93
    invoke-virtual {v0, v3}, Ljava/util/Calendar;->get(I)I

    move-result v3

    const-string v4, "mm"

    invoke-virtual {v4}, Ljava/lang/String;->length()I

    move-result v4

    invoke-static {v1, v3, v4}, Lcom/fasterxml/jackson/databind/util/ISO8601Utils;->padInt(Ljava/lang/StringBuilder;II)V

    .line 94
    invoke-virtual {v1, p0}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    const/16 v3, 0xd

    .line 95
    invoke-virtual {v0, v3}, Ljava/util/Calendar;->get(I)I

    move-result v3

    const-string v4, "ss"

    invoke-virtual {v4}, Ljava/lang/String;->length()I

    move-result v4

    invoke-static {v1, v3, v4}, Lcom/fasterxml/jackson/databind/util/ISO8601Utils;->padInt(Ljava/lang/StringBuilder;II)V

    if-eqz p1, :cond_2

    const/16 p1, 0x2e

    .line 97
    invoke-virtual {v1, p1}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    const/16 p1, 0xe

    .line 98
    invoke-virtual {v0, p1}, Ljava/util/Calendar;->get(I)I

    move-result p1

    const-string v3, "sss"

    invoke-virtual {v3}, Ljava/lang/String;->length()I

    move-result v3

    invoke-static {v1, p1, v3}, Lcom/fasterxml/jackson/databind/util/ISO8601Utils;->padInt(Ljava/lang/StringBuilder;II)V

    .line 101
    :cond_2
    invoke-virtual {v0}, Ljava/util/Calendar;->getTimeInMillis()J

    move-result-wide v3

    invoke-virtual {p2, v3, v4}, Ljava/util/TimeZone;->getOffset(J)I

    move-result p1

    if-eqz p1, :cond_4

    const p2, 0xea60

    .line 103
    div-int p2, p1, p2

    div-int/lit8 v0, p2, 0x3c

    invoke-static {v0}, Ljava/lang/Math;->abs(I)I

    move-result v0

    .line 104
    rem-int/lit8 p2, p2, 0x3c

    invoke-static {p2}, Ljava/lang/Math;->abs(I)I

    move-result p2

    if-gez p1, :cond_3

    goto :goto_3

    :cond_3
    const/16 v2, 0x2b

    .line 105
    :goto_3
    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    const-string p1, "hh"

    .line 106
    invoke-virtual {p1}, Ljava/lang/String;->length()I

    move-result p1

    invoke-static {v1, v0, p1}, Lcom/fasterxml/jackson/databind/util/ISO8601Utils;->padInt(Ljava/lang/StringBuilder;II)V

    .line 107
    invoke-virtual {v1, p0}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    const-string p0, "mm"

    .line 108
    invoke-virtual {p0}, Ljava/lang/String;->length()I

    move-result p0

    invoke-static {v1, p2, p0}, Lcom/fasterxml/jackson/databind/util/ISO8601Utils;->padInt(Ljava/lang/StringBuilder;II)V

    goto :goto_4

    :cond_4
    const/16 p0, 0x5a

    .line 110
    invoke-virtual {v1, p0}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    .line 113
    :goto_4
    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p0

    return-object p0
.end method

.method private static padInt(Ljava/lang/StringBuilder;II)V
    .locals 1

    .line 298
    invoke-static {p1}, Ljava/lang/Integer;->toString(I)Ljava/lang/String;

    move-result-object p1

    .line 299
    invoke-virtual {p1}, Ljava/lang/String;->length()I

    move-result v0

    sub-int/2addr p2, v0

    :goto_0
    if-lez p2, :cond_0

    const/16 v0, 0x30

    .line 300
    invoke-virtual {p0, v0}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    add-int/lit8 p2, p2, -0x1

    goto :goto_0

    .line 302
    :cond_0
    invoke-virtual {p0, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    return-void
.end method

.method public static parse(Ljava/lang/String;Ljava/text/ParsePosition;)Ljava/util/Date;
    .locals 17
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/text/ParseException;
        }
    .end annotation

    move-object/from16 v1, p0

    .line 134
    :try_start_0
    invoke-virtual/range {p1 .. p1}, Ljava/text/ParsePosition;->getIndex()I

    move-result v0

    add-int/lit8 v2, v0, 0x4

    .line 137
    invoke-static {v1, v0, v2}, Lcom/fasterxml/jackson/databind/util/ISO8601Utils;->parseInt(Ljava/lang/String;II)I

    move-result v0

    const/16 v3, 0x2d

    .line 138
    invoke-static {v1, v2, v3}, Lcom/fasterxml/jackson/databind/util/ISO8601Utils;->checkOffset(Ljava/lang/String;IC)Z

    move-result v4

    if-eqz v4, :cond_0

    add-int/lit8 v2, v2, 0x1

    :cond_0
    add-int/lit8 v4, v2, 0x2

    .line 143
    invoke-static {v1, v2, v4}, Lcom/fasterxml/jackson/databind/util/ISO8601Utils;->parseInt(Ljava/lang/String;II)I

    move-result v2

    .line 144
    invoke-static {v1, v4, v3}, Lcom/fasterxml/jackson/databind/util/ISO8601Utils;->checkOffset(Ljava/lang/String;IC)Z

    move-result v5

    if-eqz v5, :cond_1

    add-int/lit8 v4, v4, 0x1

    :cond_1
    add-int/lit8 v5, v4, 0x2

    .line 149
    invoke-static {v1, v4, v5}, Lcom/fasterxml/jackson/databind/util/ISO8601Utils;->parseInt(Ljava/lang/String;II)I

    move-result v4

    const/16 v6, 0x54

    .line 155
    invoke-static {v1, v5, v6}, Lcom/fasterxml/jackson/databind/util/ISO8601Utils;->checkOffset(Ljava/lang/String;IC)Z

    move-result v6

    const/16 v7, 0x5a

    const/16 v8, 0x2b

    const/4 v9, 0x0

    if-eqz v6, :cond_6

    add-int/lit8 v5, v5, 0x1

    add-int/lit8 v6, v5, 0x2

    .line 158
    invoke-static {v1, v5, v6}, Lcom/fasterxml/jackson/databind/util/ISO8601Utils;->parseInt(Ljava/lang/String;II)I

    move-result v5

    const/16 v10, 0x3a

    .line 159
    invoke-static {v1, v6, v10}, Lcom/fasterxml/jackson/databind/util/ISO8601Utils;->checkOffset(Ljava/lang/String;IC)Z

    move-result v11

    if-eqz v11, :cond_2

    add-int/lit8 v6, v6, 0x1

    :cond_2
    add-int/lit8 v11, v6, 0x2

    .line 163
    invoke-static {v1, v6, v11}, Lcom/fasterxml/jackson/databind/util/ISO8601Utils;->parseInt(Ljava/lang/String;II)I

    move-result v6

    .line 164
    invoke-static {v1, v11, v10}, Lcom/fasterxml/jackson/databind/util/ISO8601Utils;->checkOffset(Ljava/lang/String;IC)Z

    move-result v10

    if-eqz v10, :cond_3

    add-int/lit8 v11, v11, 0x1

    .line 168
    :cond_3
    invoke-virtual/range {p0 .. p0}, Ljava/lang/String;->length()I

    move-result v10

    if-le v10, v11, :cond_5

    .line 169
    invoke-virtual {v1, v11}, Ljava/lang/String;->charAt(I)C

    move-result v10

    if-eq v10, v7, :cond_5

    if-eq v10, v8, :cond_5

    if-eq v10, v3, :cond_5

    add-int/lit8 v10, v11, 0x2

    .line 171
    invoke-static {v1, v11, v10}, Lcom/fasterxml/jackson/databind/util/ISO8601Utils;->parseInt(Ljava/lang/String;II)I

    move-result v11

    const/16 v12, 0x2e

    .line 173
    invoke-static {v1, v10, v12}, Lcom/fasterxml/jackson/databind/util/ISO8601Utils;->checkOffset(Ljava/lang/String;IC)Z

    move-result v12

    if-eqz v12, :cond_4

    add-int/lit8 v10, v10, 0x1

    add-int/lit8 v12, v10, 0x3

    .line 174
    invoke-static {v1, v10, v12}, Lcom/fasterxml/jackson/databind/util/ISO8601Utils;->parseInt(Ljava/lang/String;II)I

    move-result v10

    move/from16 v16, v6

    move v6, v5

    move v5, v12

    move v12, v10

    goto :goto_0

    :cond_4
    const/4 v12, 0x0

    move/from16 v16, v6

    move v6, v5

    move v5, v10

    :goto_0
    move/from16 v10, v16

    goto :goto_1

    :cond_5
    move v10, v6

    const/4 v12, 0x0

    move v6, v5

    move v5, v11

    const/4 v11, 0x0

    goto :goto_1

    :cond_6
    const/4 v6, 0x0

    const/4 v10, 0x0

    const/4 v11, 0x0

    const/4 v12, 0x0

    .line 182
    :goto_1
    invoke-virtual/range {p0 .. p0}, Ljava/lang/String;->length()I

    move-result v13

    if-le v13, v5, :cond_c

    .line 185
    invoke-virtual {v1, v5}, Ljava/lang/String;->charAt(I)C

    move-result v13

    const/4 v14, 0x1

    if-eq v13, v8, :cond_9

    if-ne v13, v3, :cond_7

    goto :goto_2

    :cond_7
    if-ne v13, v7, :cond_8

    const-string v3, "GMT"

    add-int/2addr v5, v14

    goto :goto_3

    .line 194
    :cond_8
    new-instance v0, Ljava/lang/IndexOutOfBoundsException;

    new-instance v2, Ljava/lang/StringBuilder;

    invoke-direct {v2}, Ljava/lang/StringBuilder;-><init>()V

    const-string v3, "Invalid time zone indicator "

    invoke-virtual {v2, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v2, v13}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    invoke-virtual {v2}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v2

    invoke-direct {v0, v2}, Ljava/lang/IndexOutOfBoundsException;-><init>(Ljava/lang/String;)V

    throw v0

    .line 187
    :cond_9
    :goto_2
    invoke-virtual {v1, v5}, Ljava/lang/String;->substring(I)Ljava/lang/String;

    move-result-object v3

    .line 188
    new-instance v7, Ljava/lang/StringBuilder;

    invoke-direct {v7}, Ljava/lang/StringBuilder;-><init>()V

    const-string v8, "GMT"

    invoke-virtual {v7, v8}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v7, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v7}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v7

    .line 189
    invoke-virtual {v3}, Ljava/lang/String;->length()I

    move-result v3

    add-int/2addr v5, v3

    move-object v3, v7

    .line 197
    :goto_3
    invoke-static {v3}, Ljava/util/TimeZone;->getTimeZone(Ljava/lang/String;)Ljava/util/TimeZone;

    move-result-object v7

    .line 198
    invoke-virtual {v7}, Ljava/util/TimeZone;->getID()Ljava/lang/String;

    move-result-object v8

    .line 199
    invoke-virtual {v8, v3}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v13

    if-nez v13, :cond_b

    const-string v13, ":"

    const-string v15, ""

    .line 205
    invoke-virtual {v8, v13, v15}, Ljava/lang/String;->replace(Ljava/lang/CharSequence;Ljava/lang/CharSequence;)Ljava/lang/String;

    move-result-object v8

    .line 206
    invoke-virtual {v8, v3}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v8

    if-eqz v8, :cond_a

    goto :goto_4

    .line 207
    :cond_a
    new-instance v0, Ljava/lang/IndexOutOfBoundsException;

    new-instance v2, Ljava/lang/StringBuilder;

    invoke-direct {v2}, Ljava/lang/StringBuilder;-><init>()V

    const-string v4, "Mismatching time zone indicator: "

    invoke-virtual {v2, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v2, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v3, " given, resolves to "

    invoke-virtual {v2, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v7}, Ljava/util/TimeZone;->getID()Ljava/lang/String;

    move-result-object v3

    invoke-virtual {v2, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v2}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v2

    invoke-direct {v0, v2}, Ljava/lang/IndexOutOfBoundsException;-><init>(Ljava/lang/String;)V

    throw v0

    .line 212
    :cond_b
    :goto_4
    new-instance v3, Ljava/util/GregorianCalendar;

    invoke-direct {v3, v7}, Ljava/util/GregorianCalendar;-><init>(Ljava/util/TimeZone;)V

    .line 213
    invoke-virtual {v3, v9}, Ljava/util/Calendar;->setLenient(Z)V

    .line 214
    invoke-virtual {v3, v14, v0}, Ljava/util/Calendar;->set(II)V

    sub-int/2addr v2, v14

    const/4 v0, 0x2

    .line 215
    invoke-virtual {v3, v0, v2}, Ljava/util/Calendar;->set(II)V

    const/4 v0, 0x5

    .line 216
    invoke-virtual {v3, v0, v4}, Ljava/util/Calendar;->set(II)V

    const/16 v0, 0xb

    .line 217
    invoke-virtual {v3, v0, v6}, Ljava/util/Calendar;->set(II)V

    const/16 v0, 0xc

    .line 218
    invoke-virtual {v3, v0, v10}, Ljava/util/Calendar;->set(II)V

    const/16 v0, 0xd

    .line 219
    invoke-virtual {v3, v0, v11}, Ljava/util/Calendar;->set(II)V

    const/16 v0, 0xe

    .line 220
    invoke-virtual {v3, v0, v12}, Ljava/util/Calendar;->set(II)V
    :try_end_0
    .catch Ljava/lang/IndexOutOfBoundsException; {:try_start_0 .. :try_end_0} :catch_5
    .catch Ljava/lang/NumberFormatException; {:try_start_0 .. :try_end_0} :catch_4
    .catch Ljava/lang/IllegalArgumentException; {:try_start_0 .. :try_end_0} :catch_3

    move-object/from16 v2, p1

    .line 222
    :try_start_1
    invoke-virtual {v2, v5}, Ljava/text/ParsePosition;->setIndex(I)V

    .line 223
    invoke-virtual {v3}, Ljava/util/Calendar;->getTime()Ljava/util/Date;

    move-result-object v0

    return-object v0

    :cond_c
    move-object/from16 v2, p1

    .line 183
    new-instance v0, Ljava/lang/IllegalArgumentException;

    const-string v3, "No time zone indicator"

    invoke-direct {v0, v3}, Ljava/lang/IllegalArgumentException;-><init>(Ljava/lang/String;)V

    throw v0
    :try_end_1
    .catch Ljava/lang/IndexOutOfBoundsException; {:try_start_1 .. :try_end_1} :catch_2
    .catch Ljava/lang/NumberFormatException; {:try_start_1 .. :try_end_1} :catch_1
    .catch Ljava/lang/IllegalArgumentException; {:try_start_1 .. :try_end_1} :catch_0

    :catch_0
    move-exception v0

    goto :goto_5

    :catch_1
    move-exception v0

    goto :goto_5

    :catch_2
    move-exception v0

    goto :goto_5

    :catch_3
    move-exception v0

    move-object/from16 v2, p1

    goto :goto_5

    :catch_4
    move-exception v0

    move-object/from16 v2, p1

    goto :goto_5

    :catch_5
    move-exception v0

    move-object/from16 v2, p1

    :goto_5
    if-nez v1, :cond_d

    const/4 v1, 0x0

    goto :goto_6

    .line 233
    :cond_d
    new-instance v3, Ljava/lang/StringBuilder;

    invoke-direct {v3}, Ljava/lang/StringBuilder;-><init>()V

    const/16 v4, 0x22

    invoke-virtual {v3, v4}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    invoke-virtual {v3, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, "\'"

    invoke-virtual {v3, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v3}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    .line 234
    :goto_6
    invoke-virtual {v0}, Ljava/lang/Exception;->getMessage()Ljava/lang/String;

    move-result-object v3

    if-eqz v3, :cond_e

    .line 235
    invoke-virtual {v3}, Ljava/lang/String;->isEmpty()Z

    move-result v4

    if-eqz v4, :cond_f

    .line 236
    :cond_e
    new-instance v3, Ljava/lang/StringBuilder;

    invoke-direct {v3}, Ljava/lang/StringBuilder;-><init>()V

    const-string v4, "("

    invoke-virtual {v3, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    move-result-object v4

    invoke-virtual {v4}, Ljava/lang/Class;->getName()Ljava/lang/String;

    move-result-object v4

    invoke-virtual {v3, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v4, ")"

    invoke-virtual {v3, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v3}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v3

    .line 238
    :cond_f
    new-instance v4, Ljava/text/ParseException;

    new-instance v5, Ljava/lang/StringBuilder;

    invoke-direct {v5}, Ljava/lang/StringBuilder;-><init>()V

    const-string v6, "Failed to parse date ["

    invoke-virtual {v5, v6}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v5, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, "]: "

    invoke-virtual {v5, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v5, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v5}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-virtual/range {p1 .. p1}, Ljava/text/ParsePosition;->getIndex()I

    move-result v2

    invoke-direct {v4, v1, v2}, Ljava/text/ParseException;-><init>(Ljava/lang/String;I)V

    .line 239
    invoke-virtual {v4, v0}, Ljava/text/ParseException;->initCause(Ljava/lang/Throwable;)Ljava/lang/Throwable;

    .line 240
    throw v4
.end method

.method private static parseInt(Ljava/lang/String;II)I
    .locals 4
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/lang/NumberFormatException;
        }
    .end annotation

    if-ltz p1, :cond_4

    .line 265
    invoke-virtual {p0}, Ljava/lang/String;->length()I

    move-result v0

    if-gt p2, v0, :cond_4

    if-gt p1, p2, :cond_4

    const/4 v0, 0x0

    const/16 v1, 0xa

    if-ge p1, p2, :cond_1

    add-int/lit8 v0, p1, 0x1

    .line 273
    invoke-virtual {p0, p1}, Ljava/lang/String;->charAt(I)C

    move-result p1

    invoke-static {p1, v1}, Ljava/lang/Character;->digit(CI)I

    move-result p1

    if-ltz p1, :cond_0

    neg-int p1, p1

    move v3, v0

    move v0, p1

    move p1, v3

    goto :goto_0

    .line 275
    :cond_0
    new-instance p1, Ljava/lang/NumberFormatException;

    new-instance p2, Ljava/lang/StringBuilder;

    invoke-direct {p2}, Ljava/lang/StringBuilder;-><init>()V

    const-string v0, "Invalid number: "

    invoke-virtual {p2, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p2, p0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p2}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p0

    invoke-direct {p1, p0}, Ljava/lang/NumberFormatException;-><init>(Ljava/lang/String;)V

    throw p1

    :cond_1
    :goto_0
    if-ge p1, p2, :cond_3

    add-int/lit8 v2, p1, 0x1

    .line 280
    invoke-virtual {p0, p1}, Ljava/lang/String;->charAt(I)C

    move-result p1

    invoke-static {p1, v1}, Ljava/lang/Character;->digit(CI)I

    move-result p1

    if-ltz p1, :cond_2

    mul-int/lit8 v0, v0, 0xa

    sub-int/2addr v0, p1

    move p1, v2

    goto :goto_0

    .line 282
    :cond_2
    new-instance p1, Ljava/lang/NumberFormatException;

    new-instance p2, Ljava/lang/StringBuilder;

    invoke-direct {p2}, Ljava/lang/StringBuilder;-><init>()V

    const-string v0, "Invalid number: "

    invoke-virtual {p2, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p2, p0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p2}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p0

    invoke-direct {p1, p0}, Ljava/lang/NumberFormatException;-><init>(Ljava/lang/String;)V

    throw p1

    :cond_3
    neg-int p0, v0

    return p0

    .line 266
    :cond_4
    new-instance p1, Ljava/lang/NumberFormatException;

    invoke-direct {p1, p0}, Ljava/lang/NumberFormatException;-><init>(Ljava/lang/String;)V

    throw p1
.end method

.method public static timeZoneGMT()Ljava/util/TimeZone;
    .locals 1

    .line 37
    sget-object v0, Lcom/fasterxml/jackson/databind/util/ISO8601Utils;->TIMEZONE_GMT:Ljava/util/TimeZone;

    return-object v0
.end method
