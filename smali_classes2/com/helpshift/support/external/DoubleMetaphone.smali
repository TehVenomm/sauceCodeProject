.class public Lcom/helpshift/support/external/DoubleMetaphone;
.super Ljava/lang/Object;
.source "DoubleMetaphone.java"


# annotations
.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;
    }
.end annotation


# static fields
.field private static final ES_EP_EB_EL_EY_IB_IL_IN_IE_EI_ER:[Ljava/lang/String;

.field private static final L_R_N_M_B_H_F_V_W_SPACE:[Ljava/lang/String;

.field private static final L_T_K_S_N_M_B_Z:[Ljava/lang/String;

.field private static final SILENT_START:[Ljava/lang/String;

.field private static final VOWELS:Ljava/lang/String; = "AEIOUY"


# instance fields
.field maxCodeLen:I


# direct methods
.method static constructor <clinit>()V
    .locals 12

    const-string v0, "GN"

    const-string v1, "KN"

    const-string v2, "PN"

    const-string v3, "WR"

    const-string v4, "PS"

    .line 48
    filled-new-array {v0, v1, v2, v3, v4}, [Ljava/lang/String;

    move-result-object v0

    sput-object v0, Lcom/helpshift/support/external/DoubleMetaphone;->SILENT_START:[Ljava/lang/String;

    const-string v1, "L"

    const-string v2, "R"

    const-string v3, "N"

    const-string v4, "M"

    const-string v5, "B"

    const-string v6, "H"

    const-string v7, "F"

    const-string v8, "V"

    const-string v9, "W"

    const-string v10, " "

    .line 50
    filled-new-array/range {v1 .. v10}, [Ljava/lang/String;

    move-result-object v0

    sput-object v0, Lcom/helpshift/support/external/DoubleMetaphone;->L_R_N_M_B_H_F_V_W_SPACE:[Ljava/lang/String;

    const-string v1, "ES"

    const-string v2, "EP"

    const-string v3, "EB"

    const-string v4, "EL"

    const-string v5, "EY"

    const-string v6, "IB"

    const-string v7, "IL"

    const-string v8, "IN"

    const-string v9, "IE"

    const-string v10, "EI"

    const-string v11, "ER"

    .line 52
    filled-new-array/range {v1 .. v11}, [Ljava/lang/String;

    move-result-object v0

    sput-object v0, Lcom/helpshift/support/external/DoubleMetaphone;->ES_EP_EB_EL_EY_IB_IL_IN_IE_EI_ER:[Ljava/lang/String;

    const-string v1, "L"

    const-string v2, "T"

    const-string v3, "K"

    const-string v4, "S"

    const-string v5, "N"

    const-string v6, "M"

    const-string v7, "B"

    const-string v8, "Z"

    .line 54
    filled-new-array/range {v1 .. v8}, [Ljava/lang/String;

    move-result-object v0

    sput-object v0, Lcom/helpshift/support/external/DoubleMetaphone;->L_T_K_S_N_M_B_Z:[Ljava/lang/String;

    return-void
.end method

.method public constructor <init>()V
    .locals 1

    .line 66
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    const/4 v0, 0x4

    .line 60
    iput v0, p0, Lcom/helpshift/support/external/DoubleMetaphone;->maxCodeLen:I

    return-void
.end method

.method protected static contains(Ljava/lang/String;II[Ljava/lang/String;)Z
    .locals 2

    const/4 v0, 0x0

    if-ltz p1, :cond_1

    add-int/2addr p2, p1

    .line 76
    invoke-virtual {p0}, Ljava/lang/String;->length()I

    move-result v1

    if-gt p2, v1, :cond_1

    .line 77
    invoke-virtual {p0, p1, p2}, Ljava/lang/String;->substring(II)Ljava/lang/String;

    move-result-object p0

    .line 79
    array-length p1, p3

    const/4 p2, 0x0

    :goto_0
    if-ge p2, p1, :cond_1

    aget-object v1, p3, p2

    .line 80
    invoke-virtual {p0, v1}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v1

    if-eqz v1, :cond_0

    const/4 v0, 0x1

    goto :goto_1

    :cond_0
    add-int/lit8 p2, p2, 0x1

    goto :goto_0

    :cond_1
    :goto_1
    return v0
.end method


# virtual methods
.method protected charAt(Ljava/lang/String;I)C
    .locals 1

    if-ltz p2, :cond_1

    .line 1310
    invoke-virtual {p1}, Ljava/lang/String;->length()I

    move-result v0

    if-lt p2, v0, :cond_0

    goto :goto_0

    .line 1313
    :cond_0
    invoke-virtual {p1, p2}, Ljava/lang/String;->charAt(I)C

    move-result p1

    return p1

    :cond_1
    :goto_0
    const/4 p1, 0x0

    return p1
.end method

.method public doubleMetaphone(Ljava/lang/String;Z)Ljava/lang/String;
    .locals 22

    move-object/from16 v0, p0

    if-nez p1, :cond_0

    const/4 v1, 0x0

    return-object v1

    .line 106
    :cond_0
    invoke-virtual/range {p1 .. p1}, Ljava/lang/String;->trim()Ljava/lang/String;

    move-result-object v1

    .line 107
    invoke-virtual {v1}, Ljava/lang/String;->length()I

    move-result v2

    if-nez v2, :cond_1

    const/4 v1, 0x0

    return-object v1

    .line 110
    :cond_1
    sget-object v2, Ljava/util/Locale;->ENGLISH:Ljava/util/Locale;

    invoke-virtual {v1, v2}, Ljava/lang/String;->toUpperCase(Ljava/util/Locale;)Ljava/lang/String;

    move-result-object v1

    const/16 v2, 0x57

    .line 112
    invoke-virtual {v1, v2}, Ljava/lang/String;->indexOf(I)I

    move-result v2

    const/4 v3, -0x1

    const/16 v4, 0x4b

    const/4 v5, 0x0

    const/4 v6, 0x1

    if-gt v2, v3, :cond_3

    invoke-virtual {v1, v4}, Ljava/lang/String;->indexOf(I)I

    move-result v2

    if-gt v2, v3, :cond_3

    const-string v2, "CZ"

    .line 113
    invoke-virtual {v1, v2}, Ljava/lang/String;->indexOf(Ljava/lang/String;)I

    move-result v2

    if-gt v2, v3, :cond_3

    const-string v2, "WITZ"

    invoke-virtual {v1, v2}, Ljava/lang/String;->indexOf(Ljava/lang/String;)I

    move-result v2

    if-le v2, v3, :cond_2

    goto :goto_0

    :cond_2
    const/4 v2, 0x0

    goto :goto_1

    :cond_3
    :goto_0
    const/4 v2, 0x1

    .line 115
    :goto_1
    sget-object v7, Lcom/helpshift/support/external/DoubleMetaphone;->SILENT_START:[Ljava/lang/String;

    array-length v8, v7

    const/4 v9, 0x0

    :goto_2
    if-ge v9, v8, :cond_5

    aget-object v10, v7, v9

    .line 116
    invoke-virtual {v1, v10}, Ljava/lang/String;->startsWith(Ljava/lang/String;)Z

    move-result v10

    if-eqz v10, :cond_4

    const/4 v7, 0x1

    goto :goto_3

    :cond_4
    add-int/lit8 v9, v9, 0x1

    goto :goto_2

    :cond_5
    const/4 v7, 0x0

    .line 123
    :goto_3
    new-instance v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;

    iget v9, v0, Lcom/helpshift/support/external/DoubleMetaphone;->maxCodeLen:I

    invoke-direct {v8, v0, v9}, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;-><init>(Lcom/helpshift/support/external/DoubleMetaphone;I)V

    .line 125
    :goto_4
    iget-object v9, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->primary:Ljava/lang/StringBuilder;

    invoke-virtual {v9}, Ljava/lang/StringBuilder;->length()I

    move-result v9

    iget v10, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->maxLength:I

    if-lt v9, v10, :cond_6

    iget-object v9, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->alternate:Ljava/lang/StringBuilder;

    .line 126
    invoke-virtual {v9}, Ljava/lang/StringBuilder;->length()I

    move-result v9

    iget v10, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->maxLength:I

    if-ge v9, v10, :cond_117

    :cond_6
    invoke-virtual {v1}, Ljava/lang/String;->length()I

    move-result v9

    sub-int/2addr v9, v6

    if-gt v7, v9, :cond_117

    .line 127
    invoke-virtual {v1, v7}, Ljava/lang/String;->charAt(I)C

    move-result v9

    const/16 v10, 0xc7

    const/16 v11, 0x53

    if-eq v9, v10, :cond_114

    const/16 v10, 0xd1

    if-eq v9, v10, :cond_111

    const/16 v10, 0x41

    const/16 v12, 0x54

    const/16 v13, 0x48

    const/16 v15, 0x4a

    const/4 v4, 0x3

    const/4 v14, 0x2

    packed-switch v9, :pswitch_data_0

    add-int/lit8 v7, v7, 0x1

    :cond_7
    :goto_5
    const/16 v4, 0x4b

    goto :goto_4

    :pswitch_0
    add-int/lit8 v4, v7, 0x1

    .line 1255
    invoke-virtual {v0, v1, v4}, Lcom/helpshift/support/external/DoubleMetaphone;->charAt(Ljava/lang/String;I)C

    move-result v9

    if-ne v9, v13, :cond_a

    .line 1257
    iget-object v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->primary:Ljava/lang/StringBuilder;

    invoke-virtual {v4}, Ljava/lang/StringBuilder;->length()I

    move-result v4

    iget v9, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->maxLength:I

    if-ge v4, v9, :cond_8

    .line 1258
    iget-object v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->primary:Ljava/lang/StringBuilder;

    invoke-virtual {v4, v15}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    .line 1260
    :cond_8
    iget-object v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->alternate:Ljava/lang/StringBuilder;

    invoke-virtual {v4}, Ljava/lang/StringBuilder;->length()I

    move-result v4

    iget v9, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->maxLength:I

    if-ge v4, v9, :cond_9

    .line 1261
    iget-object v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->alternate:Ljava/lang/StringBuilder;

    invoke-virtual {v4, v15}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    :cond_9
    add-int/lit8 v7, v7, 0x2

    goto :goto_5

    :cond_a
    const-string v9, "ZO"

    const-string v10, "ZI"

    const-string v13, "ZA"

    .line 1266
    filled-new-array {v9, v10, v13}, [Ljava/lang/String;

    move-result-object v9

    invoke-static {v1, v4, v14, v9}, Lcom/helpshift/support/external/DoubleMetaphone;->contains(Ljava/lang/String;II[Ljava/lang/String;)Z

    move-result v9

    if-nez v9, :cond_d

    if-eqz v2, :cond_b

    if-lez v7, :cond_b

    add-int/lit8 v9, v7, -0x1

    .line 1267
    invoke-virtual {v0, v1, v9}, Lcom/helpshift/support/external/DoubleMetaphone;->charAt(Ljava/lang/String;I)C

    move-result v9

    if-eq v9, v12, :cond_b

    goto :goto_6

    .line 1284
    :cond_b
    iget-object v9, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->primary:Ljava/lang/StringBuilder;

    invoke-virtual {v9}, Ljava/lang/StringBuilder;->length()I

    move-result v9

    iget v10, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->maxLength:I

    if-ge v9, v10, :cond_c

    .line 1285
    iget-object v9, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->primary:Ljava/lang/StringBuilder;

    invoke-virtual {v9, v11}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    .line 1287
    :cond_c
    iget-object v9, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->alternate:Ljava/lang/StringBuilder;

    invoke-virtual {v9}, Ljava/lang/StringBuilder;->length()I

    move-result v9

    iget v10, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->maxLength:I

    if-ge v9, v10, :cond_10

    .line 1288
    iget-object v9, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->alternate:Ljava/lang/StringBuilder;

    invoke-virtual {v9, v11}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    goto :goto_8

    .line 1268
    :cond_d
    :goto_6
    iget v9, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->maxLength:I

    iget-object v10, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->primary:Ljava/lang/StringBuilder;

    invoke-virtual {v10}, Ljava/lang/StringBuilder;->length()I

    move-result v10

    sub-int/2addr v9, v10

    const-string v10, "S"

    .line 1269
    invoke-virtual {v10}, Ljava/lang/String;->length()I

    move-result v10

    if-gt v10, v9, :cond_e

    .line 1270
    iget-object v9, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->primary:Ljava/lang/StringBuilder;

    const-string v10, "S"

    invoke-virtual {v9, v10}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    goto :goto_7

    .line 1273
    :cond_e
    iget-object v10, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->primary:Ljava/lang/StringBuilder;

    const-string v11, "S"

    invoke-virtual {v11, v5, v9}, Ljava/lang/String;->substring(II)Ljava/lang/String;

    move-result-object v9

    invoke-virtual {v10, v9}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    .line 1275
    :goto_7
    iget v9, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->maxLength:I

    iget-object v10, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->alternate:Ljava/lang/StringBuilder;

    invoke-virtual {v10}, Ljava/lang/StringBuilder;->length()I

    move-result v10

    sub-int/2addr v9, v10

    const-string v10, "TS"

    .line 1276
    invoke-virtual {v10}, Ljava/lang/String;->length()I

    move-result v10

    if-gt v10, v9, :cond_f

    .line 1277
    iget-object v9, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->alternate:Ljava/lang/StringBuilder;

    const-string v10, "TS"

    invoke-virtual {v9, v10}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    goto :goto_8

    .line 1280
    :cond_f
    iget-object v10, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->alternate:Ljava/lang/StringBuilder;

    const-string v11, "TS"

    invoke-virtual {v11, v5, v9}, Ljava/lang/String;->substring(II)Ljava/lang/String;

    move-result-object v9

    invoke-virtual {v10, v9}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    .line 1291
    :cond_10
    :goto_8
    invoke-virtual {v0, v1, v4}, Lcom/helpshift/support/external/DoubleMetaphone;->charAt(Ljava/lang/String;I)C

    move-result v9

    const/16 v10, 0x5a

    if-ne v9, v10, :cond_11

    add-int/lit8 v4, v7, 0x2

    :cond_11
    :goto_9
    move v7, v4

    goto/16 :goto_5

    :pswitch_1
    if-nez v7, :cond_14

    .line 1221
    iget-object v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->primary:Ljava/lang/StringBuilder;

    invoke-virtual {v4}, Ljava/lang/StringBuilder;->length()I

    move-result v4

    iget v9, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->maxLength:I

    if-ge v4, v9, :cond_12

    .line 1222
    iget-object v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->primary:Ljava/lang/StringBuilder;

    invoke-virtual {v4, v11}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    .line 1224
    :cond_12
    iget-object v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->alternate:Ljava/lang/StringBuilder;

    invoke-virtual {v4}, Ljava/lang/StringBuilder;->length()I

    move-result v4

    iget v9, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->maxLength:I

    if-ge v4, v9, :cond_13

    .line 1225
    iget-object v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->alternate:Ljava/lang/StringBuilder;

    invoke-virtual {v4, v11}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    :cond_13
    add-int/lit8 v7, v7, 0x1

    goto/16 :goto_5

    .line 1230
    :cond_14
    invoke-virtual {v1}, Ljava/lang/String;->length()I

    move-result v9

    sub-int/2addr v9, v6

    if-ne v7, v9, :cond_15

    add-int/lit8 v9, v7, -0x3

    const-string v10, "IAU"

    const-string v11, "EAU"

    filled-new-array {v10, v11}, [Ljava/lang/String;

    move-result-object v10

    .line 1231
    invoke-static {v1, v9, v4, v10}, Lcom/helpshift/support/external/DoubleMetaphone;->contains(Ljava/lang/String;II[Ljava/lang/String;)Z

    move-result v4

    if-nez v4, :cond_18

    add-int/lit8 v4, v7, -0x2

    const-string v9, "AU"

    const-string v10, "OU"

    filled-new-array {v9, v10}, [Ljava/lang/String;

    move-result-object v9

    .line 1232
    invoke-static {v1, v4, v14, v9}, Lcom/helpshift/support/external/DoubleMetaphone;->contains(Ljava/lang/String;II[Ljava/lang/String;)Z

    move-result v4

    if-nez v4, :cond_18

    .line 1234
    :cond_15
    iget v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->maxLength:I

    iget-object v9, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->primary:Ljava/lang/StringBuilder;

    invoke-virtual {v9}, Ljava/lang/StringBuilder;->length()I

    move-result v9

    sub-int/2addr v4, v9

    const-string v9, "KS"

    .line 1235
    invoke-virtual {v9}, Ljava/lang/String;->length()I

    move-result v9

    if-gt v9, v4, :cond_16

    .line 1236
    iget-object v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->primary:Ljava/lang/StringBuilder;

    const-string v9, "KS"

    invoke-virtual {v4, v9}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    goto :goto_a

    .line 1239
    :cond_16
    iget-object v9, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->primary:Ljava/lang/StringBuilder;

    const-string v10, "KS"

    invoke-virtual {v10, v5, v4}, Ljava/lang/String;->substring(II)Ljava/lang/String;

    move-result-object v4

    invoke-virtual {v9, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    .line 1241
    :goto_a
    iget v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->maxLength:I

    iget-object v9, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->alternate:Ljava/lang/StringBuilder;

    invoke-virtual {v9}, Ljava/lang/StringBuilder;->length()I

    move-result v9

    sub-int/2addr v4, v9

    const-string v9, "KS"

    .line 1242
    invoke-virtual {v9}, Ljava/lang/String;->length()I

    move-result v9

    if-gt v9, v4, :cond_17

    .line 1243
    iget-object v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->alternate:Ljava/lang/StringBuilder;

    const-string v9, "KS"

    invoke-virtual {v4, v9}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    goto :goto_b

    .line 1246
    :cond_17
    iget-object v9, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->alternate:Ljava/lang/StringBuilder;

    const-string v10, "KS"

    invoke-virtual {v10, v5, v4}, Ljava/lang/String;->substring(II)Ljava/lang/String;

    move-result-object v4

    invoke-virtual {v9, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    :cond_18
    :goto_b
    add-int/lit8 v4, v7, 0x1

    const-string v9, "C"

    const-string v10, "X"

    .line 1249
    filled-new-array {v9, v10}, [Ljava/lang/String;

    move-result-object v9

    invoke-static {v1, v4, v6, v9}, Lcom/helpshift/support/external/DoubleMetaphone;->contains(Ljava/lang/String;II[Ljava/lang/String;)Z

    move-result v9

    if-eqz v9, :cond_11

    add-int/lit8 v4, v7, 0x2

    goto/16 :goto_9

    :pswitch_2
    const-string v9, "WR"

    .line 1151
    filled-new-array {v9}, [Ljava/lang/String;

    move-result-object v9

    invoke-static {v1, v7, v14, v9}, Lcom/helpshift/support/external/DoubleMetaphone;->contains(Ljava/lang/String;II[Ljava/lang/String;)Z

    move-result v9

    if-eqz v9, :cond_1b

    .line 1153
    iget-object v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->primary:Ljava/lang/StringBuilder;

    invoke-virtual {v4}, Ljava/lang/StringBuilder;->length()I

    move-result v4

    iget v9, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->maxLength:I

    if-ge v4, v9, :cond_19

    .line 1154
    iget-object v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->primary:Ljava/lang/StringBuilder;

    const/16 v9, 0x52

    invoke-virtual {v4, v9}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    .line 1156
    :cond_19
    iget-object v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->alternate:Ljava/lang/StringBuilder;

    invoke-virtual {v4}, Ljava/lang/StringBuilder;->length()I

    move-result v4

    iget v9, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->maxLength:I

    if-ge v4, v9, :cond_1a

    .line 1157
    iget-object v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->alternate:Ljava/lang/StringBuilder;

    const/16 v9, 0x52

    invoke-virtual {v4, v9}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    :cond_1a
    add-int/lit8 v7, v7, 0x2

    goto/16 :goto_5

    :cond_1b
    if-nez v7, :cond_21

    const-string v9, "AEIOUY"

    add-int/lit8 v11, v7, 0x1

    .line 1162
    invoke-virtual {v0, v1, v11}, Lcom/helpshift/support/external/DoubleMetaphone;->charAt(Ljava/lang/String;I)C

    move-result v12

    invoke-virtual {v9, v12}, Ljava/lang/String;->indexOf(I)I

    move-result v9

    if-ne v9, v3, :cond_1c

    const-string v9, "WH"

    filled-new-array {v9}, [Ljava/lang/String;

    move-result-object v9

    .line 1163
    invoke-static {v1, v7, v14, v9}, Lcom/helpshift/support/external/DoubleMetaphone;->contains(Ljava/lang/String;II[Ljava/lang/String;)Z

    move-result v9

    if-eqz v9, :cond_21

    :cond_1c
    const-string v4, "AEIOUY"

    .line 1164
    invoke-virtual {v0, v1, v11}, Lcom/helpshift/support/external/DoubleMetaphone;->charAt(Ljava/lang/String;I)C

    move-result v7

    invoke-virtual {v4, v7}, Ljava/lang/String;->indexOf(I)I

    move-result v4

    if-eq v4, v3, :cond_1e

    .line 1166
    iget-object v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->primary:Ljava/lang/StringBuilder;

    invoke-virtual {v4}, Ljava/lang/StringBuilder;->length()I

    move-result v4

    iget v7, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->maxLength:I

    if-ge v4, v7, :cond_1d

    .line 1167
    iget-object v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->primary:Ljava/lang/StringBuilder;

    invoke-virtual {v4, v10}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    .line 1169
    :cond_1d
    iget-object v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->alternate:Ljava/lang/StringBuilder;

    invoke-virtual {v4}, Ljava/lang/StringBuilder;->length()I

    move-result v4

    iget v7, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->maxLength:I

    if-ge v4, v7, :cond_20

    .line 1170
    iget-object v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->alternate:Ljava/lang/StringBuilder;

    const/16 v7, 0x46

    invoke-virtual {v4, v7}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    goto :goto_c

    .line 1175
    :cond_1e
    iget-object v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->primary:Ljava/lang/StringBuilder;

    invoke-virtual {v4}, Ljava/lang/StringBuilder;->length()I

    move-result v4

    iget v7, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->maxLength:I

    if-ge v4, v7, :cond_1f

    .line 1176
    iget-object v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->primary:Ljava/lang/StringBuilder;

    invoke-virtual {v4, v10}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    .line 1178
    :cond_1f
    iget-object v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->alternate:Ljava/lang/StringBuilder;

    invoke-virtual {v4}, Ljava/lang/StringBuilder;->length()I

    move-result v4

    iget v7, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->maxLength:I

    if-ge v4, v7, :cond_20

    .line 1179
    iget-object v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->alternate:Ljava/lang/StringBuilder;

    invoke-virtual {v4, v10}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    :cond_20
    :goto_c
    move v7, v11

    goto/16 :goto_5

    .line 1184
    :cond_21
    invoke-virtual {v1}, Ljava/lang/String;->length()I

    move-result v9

    sub-int/2addr v9, v6

    if-ne v7, v9, :cond_22

    const-string v9, "AEIOUY"

    add-int/lit8 v10, v7, -0x1

    invoke-virtual {v0, v1, v10}, Lcom/helpshift/support/external/DoubleMetaphone;->charAt(Ljava/lang/String;I)C

    move-result v10

    invoke-virtual {v9, v10}, Ljava/lang/String;->indexOf(I)I

    move-result v9

    if-ne v9, v3, :cond_27

    :cond_22
    add-int/lit8 v9, v7, -0x1

    const/4 v10, 0x5

    const-string v11, "EWSKI"

    const-string v12, "EWSKY"

    const-string v13, "OWSKI"

    const-string v14, "OWSKY"

    filled-new-array {v11, v12, v13, v14}, [Ljava/lang/String;

    move-result-object v11

    .line 1185
    invoke-static {v1, v9, v10, v11}, Lcom/helpshift/support/external/DoubleMetaphone;->contains(Ljava/lang/String;II[Ljava/lang/String;)Z

    move-result v9

    if-nez v9, :cond_27

    const-string v9, "SCH"

    filled-new-array {v9}, [Ljava/lang/String;

    move-result-object v9

    .line 1187
    invoke-static {v1, v5, v4, v9}, Lcom/helpshift/support/external/DoubleMetaphone;->contains(Ljava/lang/String;II[Ljava/lang/String;)Z

    move-result v4

    if-eqz v4, :cond_23

    goto :goto_f

    :cond_23
    const-string v4, "WICZ"

    const-string v9, "WITZ"

    .line 1194
    filled-new-array {v4, v9}, [Ljava/lang/String;

    move-result-object v4

    const/4 v9, 0x4

    invoke-static {v1, v7, v9, v4}, Lcom/helpshift/support/external/DoubleMetaphone;->contains(Ljava/lang/String;II[Ljava/lang/String;)Z

    move-result v4

    if-eqz v4, :cond_26

    .line 1196
    iget v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->maxLength:I

    iget-object v9, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->primary:Ljava/lang/StringBuilder;

    invoke-virtual {v9}, Ljava/lang/StringBuilder;->length()I

    move-result v9

    sub-int/2addr v4, v9

    const-string v9, "TS"

    .line 1197
    invoke-virtual {v9}, Ljava/lang/String;->length()I

    move-result v9

    if-gt v9, v4, :cond_24

    .line 1198
    iget-object v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->primary:Ljava/lang/StringBuilder;

    const-string v9, "TS"

    invoke-virtual {v4, v9}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    goto :goto_d

    .line 1201
    :cond_24
    iget-object v9, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->primary:Ljava/lang/StringBuilder;

    const-string v10, "TS"

    invoke-virtual {v10, v5, v4}, Ljava/lang/String;->substring(II)Ljava/lang/String;

    move-result-object v4

    invoke-virtual {v9, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    .line 1203
    :goto_d
    iget v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->maxLength:I

    iget-object v9, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->alternate:Ljava/lang/StringBuilder;

    invoke-virtual {v9}, Ljava/lang/StringBuilder;->length()I

    move-result v9

    sub-int/2addr v4, v9

    const-string v9, "FX"

    .line 1204
    invoke-virtual {v9}, Ljava/lang/String;->length()I

    move-result v9

    if-gt v9, v4, :cond_25

    .line 1205
    iget-object v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->alternate:Ljava/lang/StringBuilder;

    const-string v9, "FX"

    invoke-virtual {v4, v9}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    goto :goto_e

    .line 1208
    :cond_25
    iget-object v9, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->alternate:Ljava/lang/StringBuilder;

    const-string v10, "FX"

    invoke-virtual {v10, v5, v4}, Ljava/lang/String;->substring(II)Ljava/lang/String;

    move-result-object v4

    invoke-virtual {v9, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    :goto_e
    add-int/lit8 v7, v7, 0x4

    goto/16 :goto_5

    :cond_26
    add-int/lit8 v7, v7, 0x1

    goto/16 :goto_5

    .line 1189
    :cond_27
    :goto_f
    iget-object v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->alternate:Ljava/lang/StringBuilder;

    invoke-virtual {v4}, Ljava/lang/StringBuilder;->length()I

    move-result v4

    iget v9, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->maxLength:I

    if-ge v4, v9, :cond_28

    .line 1190
    iget-object v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->alternate:Ljava/lang/StringBuilder;

    const/16 v9, 0x46

    invoke-virtual {v4, v9}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    :cond_28
    add-int/lit8 v7, v7, 0x1

    goto/16 :goto_5

    .line 1141
    :pswitch_3
    iget-object v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->primary:Ljava/lang/StringBuilder;

    invoke-virtual {v4}, Ljava/lang/StringBuilder;->length()I

    move-result v4

    iget v9, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->maxLength:I

    if-ge v4, v9, :cond_29

    .line 1142
    iget-object v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->primary:Ljava/lang/StringBuilder;

    const/16 v9, 0x46

    invoke-virtual {v4, v9}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    goto :goto_10

    :cond_29
    const/16 v9, 0x46

    .line 1144
    :goto_10
    iget-object v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->alternate:Ljava/lang/StringBuilder;

    invoke-virtual {v4}, Ljava/lang/StringBuilder;->length()I

    move-result v4

    iget v10, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->maxLength:I

    if-ge v4, v10, :cond_2a

    .line 1145
    iget-object v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->alternate:Ljava/lang/StringBuilder;

    invoke-virtual {v4, v9}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    :cond_2a
    add-int/lit8 v4, v7, 0x1

    .line 1147
    invoke-virtual {v0, v1, v4}, Lcom/helpshift/support/external/DoubleMetaphone;->charAt(Ljava/lang/String;I)C

    move-result v9

    const/16 v10, 0x56

    if-ne v9, v10, :cond_11

    add-int/lit8 v7, v7, 0x2

    goto/16 :goto_5

    :pswitch_4
    const-string v9, "TION"

    .line 1088
    filled-new-array {v9}, [Ljava/lang/String;

    move-result-object v9

    const/4 v10, 0x4

    invoke-static {v1, v7, v10, v9}, Lcom/helpshift/support/external/DoubleMetaphone;->contains(Ljava/lang/String;II[Ljava/lang/String;)Z

    move-result v9

    if-eqz v9, :cond_2d

    .line 1089
    iget-object v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->primary:Ljava/lang/StringBuilder;

    invoke-virtual {v4}, Ljava/lang/StringBuilder;->length()I

    move-result v4

    iget v9, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->maxLength:I

    if-ge v4, v9, :cond_2b

    .line 1090
    iget-object v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->primary:Ljava/lang/StringBuilder;

    const/16 v9, 0x58

    invoke-virtual {v4, v9}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    goto :goto_11

    :cond_2b
    const/16 v9, 0x58

    .line 1092
    :goto_11
    iget-object v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->alternate:Ljava/lang/StringBuilder;

    invoke-virtual {v4}, Ljava/lang/StringBuilder;->length()I

    move-result v4

    iget v10, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->maxLength:I

    if-ge v4, v10, :cond_2c

    .line 1093
    iget-object v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->alternate:Ljava/lang/StringBuilder;

    invoke-virtual {v4, v9}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    :cond_2c
    add-int/lit8 v7, v7, 0x3

    goto/16 :goto_5

    :cond_2d
    const-string v9, "TIA"

    const-string v10, "TCH"

    .line 1097
    filled-new-array {v9, v10}, [Ljava/lang/String;

    move-result-object v9

    invoke-static {v1, v7, v4, v9}, Lcom/helpshift/support/external/DoubleMetaphone;->contains(Ljava/lang/String;II[Ljava/lang/String;)Z

    move-result v9

    if-eqz v9, :cond_30

    .line 1098
    iget-object v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->primary:Ljava/lang/StringBuilder;

    invoke-virtual {v4}, Ljava/lang/StringBuilder;->length()I

    move-result v4

    iget v9, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->maxLength:I

    if-ge v4, v9, :cond_2e

    .line 1099
    iget-object v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->primary:Ljava/lang/StringBuilder;

    const/16 v9, 0x58

    invoke-virtual {v4, v9}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    goto :goto_12

    :cond_2e
    const/16 v9, 0x58

    .line 1101
    :goto_12
    iget-object v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->alternate:Ljava/lang/StringBuilder;

    invoke-virtual {v4}, Ljava/lang/StringBuilder;->length()I

    move-result v4

    iget v10, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->maxLength:I

    if-ge v4, v10, :cond_2f

    .line 1102
    iget-object v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->alternate:Ljava/lang/StringBuilder;

    invoke-virtual {v4, v9}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    :cond_2f
    add-int/lit8 v7, v7, 0x3

    goto/16 :goto_5

    :cond_30
    const-string v9, "TH"

    .line 1106
    filled-new-array {v9}, [Ljava/lang/String;

    move-result-object v9

    invoke-static {v1, v7, v14, v9}, Lcom/helpshift/support/external/DoubleMetaphone;->contains(Ljava/lang/String;II[Ljava/lang/String;)Z

    move-result v9

    if-nez v9, :cond_34

    const-string v9, "TTH"

    filled-new-array {v9}, [Ljava/lang/String;

    move-result-object v9

    .line 1107
    invoke-static {v1, v7, v4, v9}, Lcom/helpshift/support/external/DoubleMetaphone;->contains(Ljava/lang/String;II[Ljava/lang/String;)Z

    move-result v9

    if-eqz v9, :cond_31

    goto :goto_13

    .line 1130
    :cond_31
    iget-object v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->primary:Ljava/lang/StringBuilder;

    invoke-virtual {v4}, Ljava/lang/StringBuilder;->length()I

    move-result v4

    iget v9, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->maxLength:I

    if-ge v4, v9, :cond_32

    .line 1131
    iget-object v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->primary:Ljava/lang/StringBuilder;

    invoke-virtual {v4, v12}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    .line 1133
    :cond_32
    iget-object v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->alternate:Ljava/lang/StringBuilder;

    invoke-virtual {v4}, Ljava/lang/StringBuilder;->length()I

    move-result v4

    iget v9, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->maxLength:I

    if-ge v4, v9, :cond_33

    .line 1134
    iget-object v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->alternate:Ljava/lang/StringBuilder;

    invoke-virtual {v4, v12}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    :cond_33
    add-int/lit8 v4, v7, 0x1

    const-string v9, "T"

    const-string v10, "D"

    .line 1136
    filled-new-array {v9, v10}, [Ljava/lang/String;

    move-result-object v9

    invoke-static {v1, v4, v6, v9}, Lcom/helpshift/support/external/DoubleMetaphone;->contains(Ljava/lang/String;II[Ljava/lang/String;)Z

    move-result v9

    if-eqz v9, :cond_11

    add-int/lit8 v4, v7, 0x2

    goto/16 :goto_9

    :cond_34
    :goto_13
    add-int/lit8 v7, v7, 0x2

    const-string v9, "OM"

    const-string v10, "AM"

    .line 1108
    filled-new-array {v9, v10}, [Ljava/lang/String;

    move-result-object v9

    invoke-static {v1, v7, v14, v9}, Lcom/helpshift/support/external/DoubleMetaphone;->contains(Ljava/lang/String;II[Ljava/lang/String;)Z

    move-result v9

    if-nez v9, :cond_37

    const-string v9, "VAN "

    const-string v10, "VON "

    filled-new-array {v9, v10}, [Ljava/lang/String;

    move-result-object v9

    const/4 v10, 0x4

    .line 1110
    invoke-static {v1, v5, v10, v9}, Lcom/helpshift/support/external/DoubleMetaphone;->contains(Ljava/lang/String;II[Ljava/lang/String;)Z

    move-result v9

    if-nez v9, :cond_37

    const-string v9, "SCH"

    filled-new-array {v9}, [Ljava/lang/String;

    move-result-object v9

    .line 1111
    invoke-static {v1, v5, v4, v9}, Lcom/helpshift/support/external/DoubleMetaphone;->contains(Ljava/lang/String;II[Ljava/lang/String;)Z

    move-result v4

    if-eqz v4, :cond_35

    goto :goto_14

    .line 1120
    :cond_35
    iget-object v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->primary:Ljava/lang/StringBuilder;

    invoke-virtual {v4}, Ljava/lang/StringBuilder;->length()I

    move-result v4

    iget v9, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->maxLength:I

    if-ge v4, v9, :cond_36

    .line 1121
    iget-object v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->primary:Ljava/lang/StringBuilder;

    const/16 v9, 0x30

    invoke-virtual {v4, v9}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    .line 1123
    :cond_36
    iget-object v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->alternate:Ljava/lang/StringBuilder;

    invoke-virtual {v4}, Ljava/lang/StringBuilder;->length()I

    move-result v4

    iget v9, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->maxLength:I

    if-ge v4, v9, :cond_7

    .line 1124
    iget-object v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->alternate:Ljava/lang/StringBuilder;

    invoke-virtual {v4, v12}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    goto/16 :goto_5

    .line 1112
    :cond_37
    :goto_14
    iget-object v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->primary:Ljava/lang/StringBuilder;

    invoke-virtual {v4}, Ljava/lang/StringBuilder;->length()I

    move-result v4

    iget v9, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->maxLength:I

    if-ge v4, v9, :cond_38

    .line 1113
    iget-object v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->primary:Ljava/lang/StringBuilder;

    invoke-virtual {v4, v12}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    .line 1115
    :cond_38
    iget-object v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->alternate:Ljava/lang/StringBuilder;

    invoke-virtual {v4}, Ljava/lang/StringBuilder;->length()I

    move-result v4

    iget v9, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->maxLength:I

    if-ge v4, v9, :cond_7

    .line 1116
    iget-object v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->alternate:Ljava/lang/StringBuilder;

    invoke-virtual {v4, v12}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    goto/16 :goto_5

    :pswitch_5
    add-int/lit8 v9, v7, -0x1

    const-string v10, "ISL"

    const-string v12, "YSL"

    .line 910
    filled-new-array {v10, v12}, [Ljava/lang/String;

    move-result-object v10

    invoke-static {v1, v9, v4, v10}, Lcom/helpshift/support/external/DoubleMetaphone;->contains(Ljava/lang/String;II[Ljava/lang/String;)Z

    move-result v9

    if-eqz v9, :cond_39

    add-int/lit8 v7, v7, 0x1

    goto/16 :goto_54

    :cond_39
    if-nez v7, :cond_3c

    const/4 v9, 0x5

    const-string v10, "SUGAR"

    .line 914
    filled-new-array {v10}, [Ljava/lang/String;

    move-result-object v10

    invoke-static {v1, v7, v9, v10}, Lcom/helpshift/support/external/DoubleMetaphone;->contains(Ljava/lang/String;II[Ljava/lang/String;)Z

    move-result v9

    if-eqz v9, :cond_3c

    .line 916
    iget-object v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->primary:Ljava/lang/StringBuilder;

    invoke-virtual {v4}, Ljava/lang/StringBuilder;->length()I

    move-result v4

    iget v9, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->maxLength:I

    if-ge v4, v9, :cond_3a

    .line 917
    iget-object v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->primary:Ljava/lang/StringBuilder;

    const/16 v9, 0x58

    invoke-virtual {v4, v9}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    .line 919
    :cond_3a
    iget-object v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->alternate:Ljava/lang/StringBuilder;

    invoke-virtual {v4}, Ljava/lang/StringBuilder;->length()I

    move-result v4

    iget v9, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->maxLength:I

    if-ge v4, v9, :cond_3b

    .line 920
    iget-object v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->alternate:Ljava/lang/StringBuilder;

    invoke-virtual {v4, v11}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    :cond_3b
    add-int/lit8 v7, v7, 0x1

    goto/16 :goto_54

    :cond_3c
    const-string v9, "SH"

    .line 924
    filled-new-array {v9}, [Ljava/lang/String;

    move-result-object v9

    invoke-static {v1, v7, v14, v9}, Lcom/helpshift/support/external/DoubleMetaphone;->contains(Ljava/lang/String;II[Ljava/lang/String;)Z

    move-result v9

    if-eqz v9, :cond_41

    add-int/lit8 v4, v7, 0x1

    const-string v9, "HEIM"

    const-string v10, "HOEK"

    const-string v12, "HOLM"

    const-string v13, "HOLZ"

    .line 925
    filled-new-array {v9, v10, v12, v13}, [Ljava/lang/String;

    move-result-object v9

    const/4 v10, 0x4

    invoke-static {v1, v4, v10, v9}, Lcom/helpshift/support/external/DoubleMetaphone;->contains(Ljava/lang/String;II[Ljava/lang/String;)Z

    move-result v4

    if-eqz v4, :cond_3e

    .line 928
    iget-object v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->primary:Ljava/lang/StringBuilder;

    invoke-virtual {v4}, Ljava/lang/StringBuilder;->length()I

    move-result v4

    iget v9, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->maxLength:I

    if-ge v4, v9, :cond_3d

    .line 929
    iget-object v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->primary:Ljava/lang/StringBuilder;

    invoke-virtual {v4, v11}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    .line 931
    :cond_3d
    iget-object v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->alternate:Ljava/lang/StringBuilder;

    invoke-virtual {v4}, Ljava/lang/StringBuilder;->length()I

    move-result v4

    iget v9, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->maxLength:I

    if-ge v4, v9, :cond_40

    .line 932
    iget-object v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->alternate:Ljava/lang/StringBuilder;

    invoke-virtual {v4, v11}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    goto :goto_16

    .line 936
    :cond_3e
    iget-object v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->primary:Ljava/lang/StringBuilder;

    invoke-virtual {v4}, Ljava/lang/StringBuilder;->length()I

    move-result v4

    iget v9, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->maxLength:I

    if-ge v4, v9, :cond_3f

    .line 937
    iget-object v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->primary:Ljava/lang/StringBuilder;

    const/16 v9, 0x58

    invoke-virtual {v4, v9}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    goto :goto_15

    :cond_3f
    const/16 v9, 0x58

    .line 939
    :goto_15
    iget-object v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->alternate:Ljava/lang/StringBuilder;

    invoke-virtual {v4}, Ljava/lang/StringBuilder;->length()I

    move-result v4

    iget v10, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->maxLength:I

    if-ge v4, v10, :cond_40

    .line 940
    iget-object v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->alternate:Ljava/lang/StringBuilder;

    invoke-virtual {v4, v9}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    :cond_40
    :goto_16
    add-int/lit8 v7, v7, 0x2

    goto/16 :goto_54

    :cond_41
    const-string v9, "SIO"

    const-string v10, "SIA"

    .line 945
    filled-new-array {v9, v10}, [Ljava/lang/String;

    move-result-object v9

    invoke-static {v1, v7, v4, v9}, Lcom/helpshift/support/external/DoubleMetaphone;->contains(Ljava/lang/String;II[Ljava/lang/String;)Z

    move-result v9

    if-nez v9, :cond_5c

    const-string v9, "SIAN"

    filled-new-array {v9}, [Ljava/lang/String;

    move-result-object v9

    const/4 v10, 0x4

    .line 946
    invoke-static {v1, v7, v10, v9}, Lcom/helpshift/support/external/DoubleMetaphone;->contains(Ljava/lang/String;II[Ljava/lang/String;)Z

    move-result v9

    if-eqz v9, :cond_42

    goto/16 :goto_1e

    :cond_42
    if-nez v7, :cond_43

    add-int/lit8 v9, v7, 0x1

    const-string v10, "M"

    const-string v12, "N"

    const-string v15, "L"

    const-string v3, "W"

    .line 966
    filled-new-array {v10, v12, v15, v3}, [Ljava/lang/String;

    move-result-object v3

    invoke-static {v1, v9, v6, v3}, Lcom/helpshift/support/external/DoubleMetaphone;->contains(Ljava/lang/String;II[Ljava/lang/String;)Z

    move-result v3

    if-nez v3, :cond_44

    :cond_43
    add-int/lit8 v3, v7, 0x1

    const-string v9, "Z"

    filled-new-array {v9}, [Ljava/lang/String;

    move-result-object v9

    .line 968
    invoke-static {v1, v3, v6, v9}, Lcom/helpshift/support/external/DoubleMetaphone;->contains(Ljava/lang/String;II[Ljava/lang/String;)Z

    move-result v9

    if-eqz v9, :cond_48

    .line 973
    :cond_44
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->primary:Ljava/lang/StringBuilder;

    invoke-virtual {v3}, Ljava/lang/StringBuilder;->length()I

    move-result v3

    iget v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->maxLength:I

    if-ge v3, v4, :cond_45

    .line 974
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->primary:Ljava/lang/StringBuilder;

    invoke-virtual {v3, v11}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    .line 976
    :cond_45
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->alternate:Ljava/lang/StringBuilder;

    invoke-virtual {v3}, Ljava/lang/StringBuilder;->length()I

    move-result v3

    iget v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->maxLength:I

    if-ge v3, v4, :cond_46

    .line 977
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->alternate:Ljava/lang/StringBuilder;

    const/16 v4, 0x58

    invoke-virtual {v3, v4}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    :cond_46
    add-int/lit8 v3, v7, 0x1

    const-string v4, "Z"

    .line 979
    filled-new-array {v4}, [Ljava/lang/String;

    move-result-object v4

    invoke-static {v1, v3, v6, v4}, Lcom/helpshift/support/external/DoubleMetaphone;->contains(Ljava/lang/String;II[Ljava/lang/String;)Z

    move-result v4

    if-eqz v4, :cond_47

    add-int/lit8 v3, v7, 0x2

    :cond_47
    :goto_17
    move v7, v3

    goto/16 :goto_54

    :cond_48
    const-string v9, "SC"

    .line 981
    filled-new-array {v9}, [Ljava/lang/String;

    move-result-object v9

    invoke-static {v1, v7, v14, v9}, Lcom/helpshift/support/external/DoubleMetaphone;->contains(Ljava/lang/String;II[Ljava/lang/String;)Z

    move-result v9

    if-eqz v9, :cond_58

    add-int/lit8 v3, v7, 0x2

    .line 982
    invoke-virtual {v0, v1, v3}, Lcom/helpshift/support/external/DoubleMetaphone;->charAt(Ljava/lang/String;I)C

    move-result v9

    if-ne v9, v13, :cond_52

    add-int/lit8 v3, v7, 0x3

    const-string v16, "OO"

    const-string v17, "ER"

    const-string v18, "EN"

    const-string v19, "UY"

    const-string v20, "ED"

    const-string v21, "EM"

    .line 984
    filled-new-array/range {v16 .. v21}, [Ljava/lang/String;

    move-result-object v9

    invoke-static {v1, v3, v14, v9}, Lcom/helpshift/support/external/DoubleMetaphone;->contains(Ljava/lang/String;II[Ljava/lang/String;)Z

    move-result v9

    if-eqz v9, :cond_4e

    const-string v4, "ER"

    const-string v9, "EN"

    .line 988
    filled-new-array {v4, v9}, [Ljava/lang/String;

    move-result-object v4

    invoke-static {v1, v3, v14, v4}, Lcom/helpshift/support/external/DoubleMetaphone;->contains(Ljava/lang/String;II[Ljava/lang/String;)Z

    move-result v3

    if-eqz v3, :cond_4b

    .line 990
    iget v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->maxLength:I

    iget-object v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->primary:Ljava/lang/StringBuilder;

    invoke-virtual {v4}, Ljava/lang/StringBuilder;->length()I

    move-result v4

    sub-int/2addr v3, v4

    const-string v4, "X"

    .line 991
    invoke-virtual {v4}, Ljava/lang/String;->length()I

    move-result v4

    if-gt v4, v3, :cond_49

    .line 992
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->primary:Ljava/lang/StringBuilder;

    const-string v4, "X"

    invoke-virtual {v3, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    goto :goto_18

    .line 995
    :cond_49
    iget-object v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->primary:Ljava/lang/StringBuilder;

    const-string v9, "X"

    invoke-virtual {v9, v5, v3}, Ljava/lang/String;->substring(II)Ljava/lang/String;

    move-result-object v3

    invoke-virtual {v4, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    .line 997
    :goto_18
    iget v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->maxLength:I

    iget-object v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->alternate:Ljava/lang/StringBuilder;

    invoke-virtual {v4}, Ljava/lang/StringBuilder;->length()I

    move-result v4

    sub-int/2addr v3, v4

    const-string v4, "SK"

    .line 998
    invoke-virtual {v4}, Ljava/lang/String;->length()I

    move-result v4

    if-gt v4, v3, :cond_4a

    .line 999
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->alternate:Ljava/lang/StringBuilder;

    const-string v4, "SK"

    invoke-virtual {v3, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    goto/16 :goto_1c

    .line 1002
    :cond_4a
    iget-object v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->alternate:Ljava/lang/StringBuilder;

    const-string v9, "SK"

    invoke-virtual {v9, v5, v3}, Ljava/lang/String;->substring(II)Ljava/lang/String;

    move-result-object v3

    invoke-virtual {v4, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    goto/16 :goto_1c

    .line 1006
    :cond_4b
    iget v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->maxLength:I

    iget-object v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->primary:Ljava/lang/StringBuilder;

    invoke-virtual {v4}, Ljava/lang/StringBuilder;->length()I

    move-result v4

    sub-int/2addr v3, v4

    const-string v4, "SK"

    .line 1007
    invoke-virtual {v4}, Ljava/lang/String;->length()I

    move-result v4

    if-gt v4, v3, :cond_4c

    .line 1008
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->primary:Ljava/lang/StringBuilder;

    const-string v4, "SK"

    invoke-virtual {v3, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    goto :goto_19

    .line 1011
    :cond_4c
    iget-object v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->primary:Ljava/lang/StringBuilder;

    const-string v9, "SK"

    invoke-virtual {v9, v5, v3}, Ljava/lang/String;->substring(II)Ljava/lang/String;

    move-result-object v3

    invoke-virtual {v4, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    .line 1013
    :goto_19
    iget v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->maxLength:I

    iget-object v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->alternate:Ljava/lang/StringBuilder;

    invoke-virtual {v4}, Ljava/lang/StringBuilder;->length()I

    move-result v4

    sub-int/2addr v3, v4

    const-string v4, "SK"

    .line 1014
    invoke-virtual {v4}, Ljava/lang/String;->length()I

    move-result v4

    if-gt v4, v3, :cond_4d

    .line 1015
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->alternate:Ljava/lang/StringBuilder;

    const-string v4, "SK"

    invoke-virtual {v3, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    goto/16 :goto_1c

    .line 1018
    :cond_4d
    iget-object v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->alternate:Ljava/lang/StringBuilder;

    const-string v9, "SK"

    invoke-virtual {v9, v5, v3}, Ljava/lang/String;->substring(II)Ljava/lang/String;

    move-result-object v3

    invoke-virtual {v4, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    goto/16 :goto_1c

    :cond_4e
    if-nez v7, :cond_50

    const-string v3, "AEIOUY"

    .line 1023
    invoke-virtual {v0, v1, v4}, Lcom/helpshift/support/external/DoubleMetaphone;->charAt(Ljava/lang/String;I)C

    move-result v9

    invoke-virtual {v3, v9}, Ljava/lang/String;->indexOf(I)I

    move-result v3

    const/4 v9, -0x1

    if-ne v3, v9, :cond_50

    invoke-virtual {v0, v1, v4}, Lcom/helpshift/support/external/DoubleMetaphone;->charAt(Ljava/lang/String;I)C

    move-result v3

    const/16 v4, 0x57

    if-eq v3, v4, :cond_50

    .line 1024
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->primary:Ljava/lang/StringBuilder;

    invoke-virtual {v3}, Ljava/lang/StringBuilder;->length()I

    move-result v3

    iget v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->maxLength:I

    if-ge v3, v4, :cond_4f

    .line 1025
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->primary:Ljava/lang/StringBuilder;

    const/16 v4, 0x58

    invoke-virtual {v3, v4}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    .line 1027
    :cond_4f
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->alternate:Ljava/lang/StringBuilder;

    invoke-virtual {v3}, Ljava/lang/StringBuilder;->length()I

    move-result v3

    iget v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->maxLength:I

    if-ge v3, v4, :cond_57

    .line 1028
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->alternate:Ljava/lang/StringBuilder;

    invoke-virtual {v3, v11}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    goto/16 :goto_1c

    .line 1032
    :cond_50
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->primary:Ljava/lang/StringBuilder;

    invoke-virtual {v3}, Ljava/lang/StringBuilder;->length()I

    move-result v3

    iget v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->maxLength:I

    if-ge v3, v4, :cond_51

    .line 1033
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->primary:Ljava/lang/StringBuilder;

    const/16 v4, 0x58

    invoke-virtual {v3, v4}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    goto :goto_1a

    :cond_51
    const/16 v4, 0x58

    .line 1035
    :goto_1a
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->alternate:Ljava/lang/StringBuilder;

    invoke-virtual {v3}, Ljava/lang/StringBuilder;->length()I

    move-result v3

    iget v9, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->maxLength:I

    if-ge v3, v9, :cond_57

    .line 1036
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->alternate:Ljava/lang/StringBuilder;

    invoke-virtual {v3, v4}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    goto/16 :goto_1c

    :cond_52
    const-string v4, "I"

    const-string v9, "E"

    const-string v10, "Y"

    .line 1041
    filled-new-array {v4, v9, v10}, [Ljava/lang/String;

    move-result-object v4

    invoke-static {v1, v3, v6, v4}, Lcom/helpshift/support/external/DoubleMetaphone;->contains(Ljava/lang/String;II[Ljava/lang/String;)Z

    move-result v3

    if-eqz v3, :cond_54

    .line 1042
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->primary:Ljava/lang/StringBuilder;

    invoke-virtual {v3}, Ljava/lang/StringBuilder;->length()I

    move-result v3

    iget v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->maxLength:I

    if-ge v3, v4, :cond_53

    .line 1043
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->primary:Ljava/lang/StringBuilder;

    invoke-virtual {v3, v11}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    .line 1045
    :cond_53
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->alternate:Ljava/lang/StringBuilder;

    invoke-virtual {v3}, Ljava/lang/StringBuilder;->length()I

    move-result v3

    iget v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->maxLength:I

    if-ge v3, v4, :cond_57

    .line 1046
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->alternate:Ljava/lang/StringBuilder;

    invoke-virtual {v3, v11}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    goto :goto_1c

    .line 1050
    :cond_54
    iget v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->maxLength:I

    iget-object v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->primary:Ljava/lang/StringBuilder;

    invoke-virtual {v4}, Ljava/lang/StringBuilder;->length()I

    move-result v4

    sub-int/2addr v3, v4

    const-string v4, "SK"

    .line 1051
    invoke-virtual {v4}, Ljava/lang/String;->length()I

    move-result v4

    if-gt v4, v3, :cond_55

    .line 1052
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->primary:Ljava/lang/StringBuilder;

    const-string v4, "SK"

    invoke-virtual {v3, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    goto :goto_1b

    .line 1055
    :cond_55
    iget-object v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->primary:Ljava/lang/StringBuilder;

    const-string v9, "SK"

    invoke-virtual {v9, v5, v3}, Ljava/lang/String;->substring(II)Ljava/lang/String;

    move-result-object v3

    invoke-virtual {v4, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    .line 1057
    :goto_1b
    iget v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->maxLength:I

    iget-object v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->alternate:Ljava/lang/StringBuilder;

    invoke-virtual {v4}, Ljava/lang/StringBuilder;->length()I

    move-result v4

    sub-int/2addr v3, v4

    const-string v4, "SK"

    .line 1058
    invoke-virtual {v4}, Ljava/lang/String;->length()I

    move-result v4

    if-gt v4, v3, :cond_56

    .line 1059
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->alternate:Ljava/lang/StringBuilder;

    const-string v4, "SK"

    invoke-virtual {v3, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    goto :goto_1c

    .line 1062
    :cond_56
    iget-object v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->alternate:Ljava/lang/StringBuilder;

    const-string v9, "SK"

    invoke-virtual {v9, v5, v3}, Ljava/lang/String;->substring(II)Ljava/lang/String;

    move-result-object v3

    invoke-virtual {v4, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    :cond_57
    :goto_1c
    add-int/lit8 v7, v7, 0x3

    goto/16 :goto_54

    .line 1068
    :cond_58
    invoke-virtual {v1}, Ljava/lang/String;->length()I

    move-result v4

    sub-int/2addr v4, v6

    if-ne v7, v4, :cond_59

    add-int/lit8 v4, v7, -0x2

    const-string v9, "AI"

    const-string v10, "OI"

    filled-new-array {v9, v10}, [Ljava/lang/String;

    move-result-object v9

    invoke-static {v1, v4, v14, v9}, Lcom/helpshift/support/external/DoubleMetaphone;->contains(Ljava/lang/String;II[Ljava/lang/String;)Z

    move-result v4

    if-eqz v4, :cond_59

    .line 1070
    iget-object v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->alternate:Ljava/lang/StringBuilder;

    invoke-virtual {v4}, Ljava/lang/StringBuilder;->length()I

    move-result v4

    iget v9, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->maxLength:I

    if-ge v4, v9, :cond_5b

    .line 1071
    iget-object v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->alternate:Ljava/lang/StringBuilder;

    invoke-virtual {v4, v11}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    goto :goto_1d

    .line 1075
    :cond_59
    iget-object v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->primary:Ljava/lang/StringBuilder;

    invoke-virtual {v4}, Ljava/lang/StringBuilder;->length()I

    move-result v4

    iget v9, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->maxLength:I

    if-ge v4, v9, :cond_5a

    .line 1076
    iget-object v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->primary:Ljava/lang/StringBuilder;

    invoke-virtual {v4, v11}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    .line 1078
    :cond_5a
    iget-object v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->alternate:Ljava/lang/StringBuilder;

    invoke-virtual {v4}, Ljava/lang/StringBuilder;->length()I

    move-result v4

    iget v9, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->maxLength:I

    if-ge v4, v9, :cond_5b

    .line 1079
    iget-object v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->alternate:Ljava/lang/StringBuilder;

    invoke-virtual {v4, v11}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    :cond_5b
    :goto_1d
    const-string v4, "S"

    const-string v9, "Z"

    .line 1082
    filled-new-array {v4, v9}, [Ljava/lang/String;

    move-result-object v4

    invoke-static {v1, v3, v6, v4}, Lcom/helpshift/support/external/DoubleMetaphone;->contains(Ljava/lang/String;II[Ljava/lang/String;)Z

    move-result v4

    if-eqz v4, :cond_47

    add-int/lit8 v3, v7, 0x2

    goto/16 :goto_17

    :cond_5c
    :goto_1e
    if-eqz v2, :cond_5e

    .line 949
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->primary:Ljava/lang/StringBuilder;

    invoke-virtual {v3}, Ljava/lang/StringBuilder;->length()I

    move-result v3

    iget v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->maxLength:I

    if-ge v3, v4, :cond_5d

    .line 950
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->primary:Ljava/lang/StringBuilder;

    invoke-virtual {v3, v11}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    .line 952
    :cond_5d
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->alternate:Ljava/lang/StringBuilder;

    invoke-virtual {v3}, Ljava/lang/StringBuilder;->length()I

    move-result v3

    iget v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->maxLength:I

    if-ge v3, v4, :cond_60

    .line 953
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->alternate:Ljava/lang/StringBuilder;

    invoke-virtual {v3, v11}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    goto :goto_1f

    .line 957
    :cond_5e
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->primary:Ljava/lang/StringBuilder;

    invoke-virtual {v3}, Ljava/lang/StringBuilder;->length()I

    move-result v3

    iget v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->maxLength:I

    if-ge v3, v4, :cond_5f

    .line 958
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->primary:Ljava/lang/StringBuilder;

    invoke-virtual {v3, v11}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    .line 960
    :cond_5f
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->alternate:Ljava/lang/StringBuilder;

    invoke-virtual {v3}, Ljava/lang/StringBuilder;->length()I

    move-result v3

    iget v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->maxLength:I

    if-ge v3, v4, :cond_60

    .line 961
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->alternate:Ljava/lang/StringBuilder;

    const/16 v4, 0x58

    invoke-virtual {v3, v4}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    :cond_60
    :goto_1f
    add-int/lit8 v7, v7, 0x3

    goto/16 :goto_54

    .line 891
    :pswitch_6
    invoke-virtual {v1}, Ljava/lang/String;->length()I

    move-result v3

    sub-int/2addr v3, v6

    if-ne v7, v3, :cond_61

    if-nez v2, :cond_61

    add-int/lit8 v3, v7, -0x2

    const-string v4, "IE"

    filled-new-array {v4}, [Ljava/lang/String;

    move-result-object v4

    .line 892
    invoke-static {v1, v3, v14, v4}, Lcom/helpshift/support/external/DoubleMetaphone;->contains(Ljava/lang/String;II[Ljava/lang/String;)Z

    move-result v3

    if-eqz v3, :cond_61

    add-int/lit8 v3, v7, -0x4

    const-string v4, "ME"

    const-string v9, "MA"

    filled-new-array {v4, v9}, [Ljava/lang/String;

    move-result-object v4

    .line 893
    invoke-static {v1, v3, v14, v4}, Lcom/helpshift/support/external/DoubleMetaphone;->contains(Ljava/lang/String;II[Ljava/lang/String;)Z

    move-result v3

    if-nez v3, :cond_61

    .line 894
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->alternate:Ljava/lang/StringBuilder;

    invoke-virtual {v3}, Ljava/lang/StringBuilder;->length()I

    move-result v3

    iget v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->maxLength:I

    if-ge v3, v4, :cond_63

    .line 895
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->alternate:Ljava/lang/StringBuilder;

    const/16 v4, 0x52

    invoke-virtual {v3, v4}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    goto :goto_20

    .line 899
    :cond_61
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->primary:Ljava/lang/StringBuilder;

    invoke-virtual {v3}, Ljava/lang/StringBuilder;->length()I

    move-result v3

    iget v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->maxLength:I

    if-ge v3, v4, :cond_62

    .line 900
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->primary:Ljava/lang/StringBuilder;

    const/16 v4, 0x52

    invoke-virtual {v3, v4}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    .line 902
    :cond_62
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->alternate:Ljava/lang/StringBuilder;

    invoke-virtual {v3}, Ljava/lang/StringBuilder;->length()I

    move-result v3

    iget v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->maxLength:I

    if-ge v3, v4, :cond_63

    .line 903
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->alternate:Ljava/lang/StringBuilder;

    const/16 v4, 0x52

    invoke-virtual {v3, v4}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    :cond_63
    :goto_20
    add-int/lit8 v3, v7, 0x1

    .line 906
    invoke-virtual {v0, v1, v3}, Lcom/helpshift/support/external/DoubleMetaphone;->charAt(Ljava/lang/String;I)C

    move-result v4

    const/16 v9, 0x52

    if-ne v4, v9, :cond_47

    add-int/lit8 v7, v7, 0x2

    goto/16 :goto_54

    .line 882
    :pswitch_7
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->primary:Ljava/lang/StringBuilder;

    invoke-virtual {v3}, Ljava/lang/StringBuilder;->length()I

    move-result v3

    iget v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->maxLength:I

    if-ge v3, v4, :cond_64

    .line 883
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->primary:Ljava/lang/StringBuilder;

    const/16 v4, 0x4b

    invoke-virtual {v3, v4}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    goto :goto_21

    :cond_64
    const/16 v4, 0x4b

    .line 885
    :goto_21
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->alternate:Ljava/lang/StringBuilder;

    invoke-virtual {v3}, Ljava/lang/StringBuilder;->length()I

    move-result v3

    iget v9, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->maxLength:I

    if-ge v3, v9, :cond_65

    .line 886
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->alternate:Ljava/lang/StringBuilder;

    invoke-virtual {v3, v4}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    :cond_65
    add-int/lit8 v3, v7, 0x1

    .line 888
    invoke-virtual {v0, v1, v3}, Lcom/helpshift/support/external/DoubleMetaphone;->charAt(Ljava/lang/String;I)C

    move-result v4

    const/16 v9, 0x51

    if-ne v4, v9, :cond_47

    add-int/lit8 v7, v7, 0x2

    goto/16 :goto_54

    :pswitch_8
    add-int/lit8 v3, v7, 0x1

    .line 861
    invoke-virtual {v0, v1, v3}, Lcom/helpshift/support/external/DoubleMetaphone;->charAt(Ljava/lang/String;I)C

    move-result v4

    if-ne v4, v13, :cond_68

    .line 862
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->primary:Ljava/lang/StringBuilder;

    invoke-virtual {v3}, Ljava/lang/StringBuilder;->length()I

    move-result v3

    iget v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->maxLength:I

    if-ge v3, v4, :cond_66

    .line 863
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->primary:Ljava/lang/StringBuilder;

    const/16 v4, 0x46

    invoke-virtual {v3, v4}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    goto :goto_22

    :cond_66
    const/16 v4, 0x46

    .line 865
    :goto_22
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->alternate:Ljava/lang/StringBuilder;

    invoke-virtual {v3}, Ljava/lang/StringBuilder;->length()I

    move-result v3

    iget v9, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->maxLength:I

    if-ge v3, v9, :cond_67

    .line 866
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->alternate:Ljava/lang/StringBuilder;

    invoke-virtual {v3, v4}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    :cond_67
    add-int/lit8 v7, v7, 0x2

    goto/16 :goto_54

    .line 871
    :cond_68
    iget-object v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->primary:Ljava/lang/StringBuilder;

    invoke-virtual {v4}, Ljava/lang/StringBuilder;->length()I

    move-result v4

    iget v9, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->maxLength:I

    if-ge v4, v9, :cond_69

    .line 872
    iget-object v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->primary:Ljava/lang/StringBuilder;

    const/16 v9, 0x50

    invoke-virtual {v4, v9}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    .line 874
    :cond_69
    iget-object v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->alternate:Ljava/lang/StringBuilder;

    invoke-virtual {v4}, Ljava/lang/StringBuilder;->length()I

    move-result v4

    iget v9, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->maxLength:I

    if-ge v4, v9, :cond_6a

    .line 875
    iget-object v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->alternate:Ljava/lang/StringBuilder;

    const/16 v9, 0x50

    invoke-virtual {v4, v9}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    :cond_6a
    const-string v4, "P"

    const-string v9, "B"

    .line 877
    filled-new-array {v4, v9}, [Ljava/lang/String;

    move-result-object v4

    invoke-static {v1, v3, v6, v4}, Lcom/helpshift/support/external/DoubleMetaphone;->contains(Ljava/lang/String;II[Ljava/lang/String;)Z

    move-result v4

    if-eqz v4, :cond_47

    add-int/lit8 v3, v7, 0x2

    goto/16 :goto_17

    .line 841
    :pswitch_9
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->primary:Ljava/lang/StringBuilder;

    invoke-virtual {v3}, Ljava/lang/StringBuilder;->length()I

    move-result v3

    iget v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->maxLength:I

    if-ge v3, v4, :cond_6b

    .line 842
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->primary:Ljava/lang/StringBuilder;

    const/16 v4, 0x4e

    invoke-virtual {v3, v4}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    .line 844
    :cond_6b
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->alternate:Ljava/lang/StringBuilder;

    invoke-virtual {v3}, Ljava/lang/StringBuilder;->length()I

    move-result v3

    iget v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->maxLength:I

    if-ge v3, v4, :cond_6c

    .line 845
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->alternate:Ljava/lang/StringBuilder;

    const/16 v4, 0x4e

    invoke-virtual {v3, v4}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    :cond_6c
    add-int/lit8 v3, v7, 0x1

    .line 847
    invoke-virtual {v0, v1, v3}, Lcom/helpshift/support/external/DoubleMetaphone;->charAt(Ljava/lang/String;I)C

    move-result v4

    const/16 v9, 0x4e

    if-ne v4, v9, :cond_47

    add-int/lit8 v7, v7, 0x2

    goto/16 :goto_54

    .line 823
    :pswitch_a
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->primary:Ljava/lang/StringBuilder;

    invoke-virtual {v3}, Ljava/lang/StringBuilder;->length()I

    move-result v3

    iget v9, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->maxLength:I

    if-ge v3, v9, :cond_6d

    .line 824
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->primary:Ljava/lang/StringBuilder;

    const/16 v9, 0x4d

    invoke-virtual {v3, v9}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    .line 826
    :cond_6d
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->alternate:Ljava/lang/StringBuilder;

    invoke-virtual {v3}, Ljava/lang/StringBuilder;->length()I

    move-result v3

    iget v9, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->maxLength:I

    if-ge v3, v9, :cond_6e

    .line 827
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->alternate:Ljava/lang/StringBuilder;

    const/16 v9, 0x4d

    invoke-virtual {v3, v9}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    :cond_6e
    add-int/lit8 v3, v7, 0x1

    .line 830
    invoke-virtual {v0, v1, v3}, Lcom/helpshift/support/external/DoubleMetaphone;->charAt(Ljava/lang/String;I)C

    move-result v9

    const/16 v10, 0x4d

    if-ne v9, v10, :cond_70

    :cond_6f
    :goto_23
    const/4 v4, 0x1

    goto :goto_24

    :cond_70
    add-int/lit8 v9, v7, -0x1

    const-string v10, "UMB"

    .line 834
    filled-new-array {v10}, [Ljava/lang/String;

    move-result-object v10

    invoke-static {v1, v9, v4, v10}, Lcom/helpshift/support/external/DoubleMetaphone;->contains(Ljava/lang/String;II[Ljava/lang/String;)Z

    move-result v4

    if-eqz v4, :cond_71

    .line 835
    invoke-virtual {v1}, Ljava/lang/String;->length()I

    move-result v4

    sub-int/2addr v4, v6

    if-eq v3, v4, :cond_6f

    add-int/lit8 v4, v7, 0x2

    const-string v9, "ER"

    filled-new-array {v9}, [Ljava/lang/String;

    move-result-object v9

    .line 836
    invoke-static {v1, v4, v14, v9}, Lcom/helpshift/support/external/DoubleMetaphone;->contains(Ljava/lang/String;II[Ljava/lang/String;)Z

    move-result v4

    if-eqz v4, :cond_71

    goto :goto_23

    :cond_71
    const/4 v4, 0x0

    :goto_24
    if-eqz v4, :cond_47

    add-int/lit8 v7, v7, 0x2

    goto/16 :goto_54

    :pswitch_b
    add-int/lit8 v3, v7, 0x1

    .line 782
    invoke-virtual {v0, v1, v3}, Lcom/helpshift/support/external/DoubleMetaphone;->charAt(Ljava/lang/String;I)C

    move-result v9

    const/16 v10, 0x4c

    if-ne v9, v10, :cond_78

    .line 784
    invoke-virtual {v1}, Ljava/lang/String;->length()I

    move-result v3

    sub-int/2addr v3, v4

    if-ne v7, v3, :cond_72

    add-int/lit8 v3, v7, -0x1

    const-string v4, "ILLO"

    const-string v9, "ILLA"

    const-string v11, "ALLE"

    filled-new-array {v4, v9, v11}, [Ljava/lang/String;

    move-result-object v4

    const/4 v9, 0x4

    .line 785
    invoke-static {v1, v3, v9, v4}, Lcom/helpshift/support/external/DoubleMetaphone;->contains(Ljava/lang/String;II[Ljava/lang/String;)Z

    move-result v3

    if-eqz v3, :cond_72

    :goto_25
    const/4 v3, 0x1

    goto :goto_26

    .line 788
    :cond_72
    invoke-virtual {v1}, Ljava/lang/String;->length()I

    move-result v3

    sub-int/2addr v3, v14

    const-string v4, "AS"

    const-string v9, "OS"

    filled-new-array {v4, v9}, [Ljava/lang/String;

    move-result-object v4

    invoke-static {v1, v3, v14, v4}, Lcom/helpshift/support/external/DoubleMetaphone;->contains(Ljava/lang/String;II[Ljava/lang/String;)Z

    move-result v3

    if-nez v3, :cond_73

    .line 789
    invoke-virtual {v1}, Ljava/lang/String;->length()I

    move-result v3

    sub-int/2addr v3, v6

    const-string v4, "A"

    const-string v9, "O"

    filled-new-array {v4, v9}, [Ljava/lang/String;

    move-result-object v4

    invoke-static {v1, v3, v6, v4}, Lcom/helpshift/support/external/DoubleMetaphone;->contains(Ljava/lang/String;II[Ljava/lang/String;)Z

    move-result v3

    if-eqz v3, :cond_74

    :cond_73
    add-int/lit8 v3, v7, -0x1

    const-string v4, "ALLE"

    filled-new-array {v4}, [Ljava/lang/String;

    move-result-object v4

    const/4 v9, 0x4

    .line 790
    invoke-static {v1, v3, v9, v4}, Lcom/helpshift/support/external/DoubleMetaphone;->contains(Ljava/lang/String;II[Ljava/lang/String;)Z

    move-result v3

    if-eqz v3, :cond_74

    goto :goto_25

    :cond_74
    const/4 v3, 0x0

    :goto_26
    if-eqz v3, :cond_75

    .line 797
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->primary:Ljava/lang/StringBuilder;

    invoke-virtual {v3}, Ljava/lang/StringBuilder;->length()I

    move-result v3

    iget v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->maxLength:I

    if-ge v3, v4, :cond_77

    .line 798
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->primary:Ljava/lang/StringBuilder;

    invoke-virtual {v3, v10}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    goto :goto_27

    .line 802
    :cond_75
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->primary:Ljava/lang/StringBuilder;

    invoke-virtual {v3}, Ljava/lang/StringBuilder;->length()I

    move-result v3

    iget v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->maxLength:I

    if-ge v3, v4, :cond_76

    .line 803
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->primary:Ljava/lang/StringBuilder;

    invoke-virtual {v3, v10}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    .line 805
    :cond_76
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->alternate:Ljava/lang/StringBuilder;

    invoke-virtual {v3}, Ljava/lang/StringBuilder;->length()I

    move-result v3

    iget v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->maxLength:I

    if-ge v3, v4, :cond_77

    .line 806
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->alternate:Ljava/lang/StringBuilder;

    invoke-virtual {v3, v10}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    :cond_77
    :goto_27
    add-int/lit8 v7, v7, 0x2

    goto/16 :goto_54

    .line 813
    :cond_78
    iget-object v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->primary:Ljava/lang/StringBuilder;

    invoke-virtual {v4}, Ljava/lang/StringBuilder;->length()I

    move-result v4

    iget v7, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->maxLength:I

    if-ge v4, v7, :cond_79

    .line 814
    iget-object v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->primary:Ljava/lang/StringBuilder;

    invoke-virtual {v4, v10}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    .line 816
    :cond_79
    iget-object v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->alternate:Ljava/lang/StringBuilder;

    invoke-virtual {v4}, Ljava/lang/StringBuilder;->length()I

    move-result v4

    iget v7, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->maxLength:I

    if-ge v4, v7, :cond_47

    .line 817
    iget-object v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->alternate:Ljava/lang/StringBuilder;

    invoke-virtual {v4, v10}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    goto/16 :goto_17

    .line 772
    :pswitch_c
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->primary:Ljava/lang/StringBuilder;

    invoke-virtual {v3}, Ljava/lang/StringBuilder;->length()I

    move-result v3

    iget v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->maxLength:I

    if-ge v3, v4, :cond_7a

    .line 773
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->primary:Ljava/lang/StringBuilder;

    const/16 v4, 0x4b

    invoke-virtual {v3, v4}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    goto :goto_28

    :cond_7a
    const/16 v4, 0x4b

    .line 775
    :goto_28
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->alternate:Ljava/lang/StringBuilder;

    invoke-virtual {v3}, Ljava/lang/StringBuilder;->length()I

    move-result v3

    iget v9, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->maxLength:I

    if-ge v3, v9, :cond_7b

    .line 776
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->alternate:Ljava/lang/StringBuilder;

    invoke-virtual {v3, v4}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    :cond_7b
    add-int/lit8 v3, v7, 0x1

    .line 778
    invoke-virtual {v0, v1, v3}, Lcom/helpshift/support/external/DoubleMetaphone;->charAt(Ljava/lang/String;I)C

    move-result v9

    if-ne v9, v4, :cond_47

    add-int/lit8 v7, v7, 0x2

    goto/16 :goto_54

    :pswitch_d
    const-string v3, "JOSE"

    .line 705
    filled-new-array {v3}, [Ljava/lang/String;

    move-result-object v3

    const/4 v4, 0x4

    invoke-static {v1, v7, v4, v3}, Lcom/helpshift/support/external/DoubleMetaphone;->contains(Ljava/lang/String;II[Ljava/lang/String;)Z

    move-result v3

    if-nez v3, :cond_86

    const-string v3, "SAN "

    filled-new-array {v3}, [Ljava/lang/String;

    move-result-object v3

    invoke-static {v1, v5, v4, v3}, Lcom/helpshift/support/external/DoubleMetaphone;->contains(Ljava/lang/String;II[Ljava/lang/String;)Z

    move-result v3

    if-eqz v3, :cond_7c

    goto/16 :goto_2a

    :cond_7c
    if-nez v7, :cond_7e

    const-string v3, "JOSE"

    .line 727
    filled-new-array {v3}, [Ljava/lang/String;

    move-result-object v3

    invoke-static {v1, v7, v4, v3}, Lcom/helpshift/support/external/DoubleMetaphone;->contains(Ljava/lang/String;II[Ljava/lang/String;)Z

    move-result v3

    if-nez v3, :cond_7e

    .line 728
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->primary:Ljava/lang/StringBuilder;

    invoke-virtual {v3}, Ljava/lang/StringBuilder;->length()I

    move-result v3

    iget v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->maxLength:I

    if-ge v3, v4, :cond_7d

    .line 729
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->primary:Ljava/lang/StringBuilder;

    invoke-virtual {v3, v15}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    .line 731
    :cond_7d
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->alternate:Ljava/lang/StringBuilder;

    invoke-virtual {v3}, Ljava/lang/StringBuilder;->length()I

    move-result v3

    iget v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->maxLength:I

    if-ge v3, v4, :cond_85

    .line 732
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->alternate:Ljava/lang/StringBuilder;

    invoke-virtual {v3, v10}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    goto/16 :goto_29

    :cond_7e
    const-string v3, "AEIOUY"

    add-int/lit8 v4, v7, -0x1

    .line 735
    invoke-virtual {v0, v1, v4}, Lcom/helpshift/support/external/DoubleMetaphone;->charAt(Ljava/lang/String;I)C

    move-result v9

    invoke-virtual {v3, v9}, Ljava/lang/String;->indexOf(I)I

    move-result v3

    const/4 v9, -0x1

    if-eq v3, v9, :cond_81

    if-nez v2, :cond_81

    add-int/lit8 v3, v7, 0x1

    .line 736
    invoke-virtual {v0, v1, v3}, Lcom/helpshift/support/external/DoubleMetaphone;->charAt(Ljava/lang/String;I)C

    move-result v9

    if-eq v9, v10, :cond_7f

    invoke-virtual {v0, v1, v3}, Lcom/helpshift/support/external/DoubleMetaphone;->charAt(Ljava/lang/String;I)C

    move-result v3

    const/16 v9, 0x4f

    if-ne v3, v9, :cond_81

    .line 737
    :cond_7f
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->primary:Ljava/lang/StringBuilder;

    invoke-virtual {v3}, Ljava/lang/StringBuilder;->length()I

    move-result v3

    iget v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->maxLength:I

    if-ge v3, v4, :cond_80

    .line 738
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->primary:Ljava/lang/StringBuilder;

    invoke-virtual {v3, v15}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    .line 740
    :cond_80
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->alternate:Ljava/lang/StringBuilder;

    invoke-virtual {v3}, Ljava/lang/StringBuilder;->length()I

    move-result v3

    iget v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->maxLength:I

    if-ge v3, v4, :cond_85

    .line 741
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->alternate:Ljava/lang/StringBuilder;

    invoke-virtual {v3, v13}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    goto :goto_29

    .line 744
    :cond_81
    invoke-virtual {v1}, Ljava/lang/String;->length()I

    move-result v3

    sub-int/2addr v3, v6

    if-ne v7, v3, :cond_83

    .line 745
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->primary:Ljava/lang/StringBuilder;

    invoke-virtual {v3}, Ljava/lang/StringBuilder;->length()I

    move-result v3

    iget v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->maxLength:I

    if-ge v3, v4, :cond_82

    .line 746
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->primary:Ljava/lang/StringBuilder;

    invoke-virtual {v3, v15}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    .line 748
    :cond_82
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->alternate:Ljava/lang/StringBuilder;

    invoke-virtual {v3}, Ljava/lang/StringBuilder;->length()I

    move-result v3

    iget v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->maxLength:I

    if-ge v3, v4, :cond_85

    .line 749
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->alternate:Ljava/lang/StringBuilder;

    const/16 v4, 0x20

    invoke-virtual {v3, v4}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    goto :goto_29

    :cond_83
    add-int/lit8 v3, v7, 0x1

    .line 752
    sget-object v9, Lcom/helpshift/support/external/DoubleMetaphone;->L_T_K_S_N_M_B_Z:[Ljava/lang/String;

    invoke-static {v1, v3, v6, v9}, Lcom/helpshift/support/external/DoubleMetaphone;->contains(Ljava/lang/String;II[Ljava/lang/String;)Z

    move-result v3

    if-nez v3, :cond_85

    const-string v3, "S"

    const-string v9, "K"

    const-string v10, "L"

    filled-new-array {v3, v9, v10}, [Ljava/lang/String;

    move-result-object v3

    .line 753
    invoke-static {v1, v4, v6, v3}, Lcom/helpshift/support/external/DoubleMetaphone;->contains(Ljava/lang/String;II[Ljava/lang/String;)Z

    move-result v3

    if-nez v3, :cond_85

    .line 754
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->primary:Ljava/lang/StringBuilder;

    invoke-virtual {v3}, Ljava/lang/StringBuilder;->length()I

    move-result v3

    iget v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->maxLength:I

    if-ge v3, v4, :cond_84

    .line 755
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->primary:Ljava/lang/StringBuilder;

    invoke-virtual {v3, v15}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    .line 757
    :cond_84
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->alternate:Ljava/lang/StringBuilder;

    invoke-virtual {v3}, Ljava/lang/StringBuilder;->length()I

    move-result v3

    iget v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->maxLength:I

    if-ge v3, v4, :cond_85

    .line 758
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->alternate:Ljava/lang/StringBuilder;

    invoke-virtual {v3, v15}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    :cond_85
    :goto_29
    add-int/lit8 v3, v7, 0x1

    .line 762
    invoke-virtual {v0, v1, v3}, Lcom/helpshift/support/external/DoubleMetaphone;->charAt(Ljava/lang/String;I)C

    move-result v4

    if-ne v4, v15, :cond_47

    add-int/lit8 v7, v7, 0x2

    goto/16 :goto_54

    :cond_86
    :goto_2a
    if-nez v7, :cond_87

    add-int/lit8 v3, v7, 0x4

    .line 707
    invoke-virtual {v0, v1, v3}, Lcom/helpshift/support/external/DoubleMetaphone;->charAt(Ljava/lang/String;I)C

    move-result v3

    const/16 v4, 0x20

    if-eq v3, v4, :cond_8a

    .line 708
    :cond_87
    invoke-virtual {v1}, Ljava/lang/String;->length()I

    move-result v3

    const/4 v4, 0x4

    if-eq v3, v4, :cond_8a

    const-string v3, "SAN "

    filled-new-array {v3}, [Ljava/lang/String;

    move-result-object v3

    invoke-static {v1, v5, v4, v3}, Lcom/helpshift/support/external/DoubleMetaphone;->contains(Ljava/lang/String;II[Ljava/lang/String;)Z

    move-result v3

    if-eqz v3, :cond_88

    goto :goto_2b

    .line 717
    :cond_88
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->primary:Ljava/lang/StringBuilder;

    invoke-virtual {v3}, Ljava/lang/StringBuilder;->length()I

    move-result v3

    iget v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->maxLength:I

    if-ge v3, v4, :cond_89

    .line 718
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->primary:Ljava/lang/StringBuilder;

    invoke-virtual {v3, v15}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    .line 720
    :cond_89
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->alternate:Ljava/lang/StringBuilder;

    invoke-virtual {v3}, Ljava/lang/StringBuilder;->length()I

    move-result v3

    iget v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->maxLength:I

    if-ge v3, v4, :cond_8c

    .line 721
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->alternate:Ljava/lang/StringBuilder;

    invoke-virtual {v3, v13}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    goto :goto_2c

    .line 709
    :cond_8a
    :goto_2b
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->primary:Ljava/lang/StringBuilder;

    invoke-virtual {v3}, Ljava/lang/StringBuilder;->length()I

    move-result v3

    iget v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->maxLength:I

    if-ge v3, v4, :cond_8b

    .line 710
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->primary:Ljava/lang/StringBuilder;

    invoke-virtual {v3, v13}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    .line 712
    :cond_8b
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->alternate:Ljava/lang/StringBuilder;

    invoke-virtual {v3}, Ljava/lang/StringBuilder;->length()I

    move-result v3

    iget v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->maxLength:I

    if-ge v3, v4, :cond_8c

    .line 713
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->alternate:Ljava/lang/StringBuilder;

    invoke-virtual {v3, v13}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    :cond_8c
    :goto_2c
    add-int/lit8 v7, v7, 0x1

    goto/16 :goto_54

    :pswitch_e
    if-eqz v7, :cond_8d

    const-string v3, "AEIOUY"

    add-int/lit8 v4, v7, -0x1

    .line 687
    invoke-virtual {v0, v1, v4}, Lcom/helpshift/support/external/DoubleMetaphone;->charAt(Ljava/lang/String;I)C

    move-result v4

    invoke-virtual {v3, v4}, Ljava/lang/String;->indexOf(I)I

    move-result v3

    const/4 v4, -0x1

    if-eq v3, v4, :cond_90

    goto :goto_2d

    :cond_8d
    const/4 v4, -0x1

    :goto_2d
    const-string v3, "AEIOUY"

    add-int/lit8 v9, v7, 0x1

    .line 688
    invoke-virtual {v0, v1, v9}, Lcom/helpshift/support/external/DoubleMetaphone;->charAt(Ljava/lang/String;I)C

    move-result v9

    invoke-virtual {v3, v9}, Ljava/lang/String;->indexOf(I)I

    move-result v3

    if-eq v3, v4, :cond_90

    .line 689
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->primary:Ljava/lang/StringBuilder;

    invoke-virtual {v3}, Ljava/lang/StringBuilder;->length()I

    move-result v3

    iget v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->maxLength:I

    if-ge v3, v4, :cond_8e

    .line 690
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->primary:Ljava/lang/StringBuilder;

    invoke-virtual {v3, v13}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    .line 692
    :cond_8e
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->alternate:Ljava/lang/StringBuilder;

    invoke-virtual {v3}, Ljava/lang/StringBuilder;->length()I

    move-result v3

    iget v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->maxLength:I

    if-ge v3, v4, :cond_8f

    .line 693
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->alternate:Ljava/lang/StringBuilder;

    invoke-virtual {v3, v13}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    :cond_8f
    add-int/lit8 v7, v7, 0x2

    goto/16 :goto_54

    :cond_90
    add-int/lit8 v7, v7, 0x1

    goto/16 :goto_54

    :pswitch_f
    add-int/lit8 v3, v7, 0x1

    .line 476
    invoke-virtual {v0, v1, v3}, Lcom/helpshift/support/external/DoubleMetaphone;->charAt(Ljava/lang/String;I)C

    move-result v9

    if-ne v9, v13, :cond_a0

    if-lez v7, :cond_93

    const-string v3, "AEIOUY"

    add-int/lit8 v9, v7, -0x1

    .line 478
    invoke-virtual {v0, v1, v9}, Lcom/helpshift/support/external/DoubleMetaphone;->charAt(Ljava/lang/String;I)C

    move-result v9

    invoke-virtual {v3, v9}, Ljava/lang/String;->indexOf(I)I

    move-result v3

    const/4 v9, -0x1

    if-ne v3, v9, :cond_93

    .line 479
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->primary:Ljava/lang/StringBuilder;

    invoke-virtual {v3}, Ljava/lang/StringBuilder;->length()I

    move-result v3

    iget v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->maxLength:I

    if-ge v3, v4, :cond_91

    .line 480
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->primary:Ljava/lang/StringBuilder;

    const/16 v4, 0x4b

    invoke-virtual {v3, v4}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    goto :goto_2e

    :cond_91
    const/16 v4, 0x4b

    .line 482
    :goto_2e
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->alternate:Ljava/lang/StringBuilder;

    invoke-virtual {v3}, Ljava/lang/StringBuilder;->length()I

    move-result v3

    iget v9, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->maxLength:I

    if-ge v3, v9, :cond_92

    .line 483
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->alternate:Ljava/lang/StringBuilder;

    invoke-virtual {v3, v4}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    :cond_92
    add-int/lit8 v7, v7, 0x2

    goto/16 :goto_54

    :cond_93
    if-nez v7, :cond_97

    add-int/lit8 v7, v7, 0x2

    .line 488
    invoke-virtual {v0, v1, v7}, Lcom/helpshift/support/external/DoubleMetaphone;->charAt(Ljava/lang/String;I)C

    move-result v3

    const/16 v4, 0x49

    if-ne v3, v4, :cond_95

    .line 489
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->primary:Ljava/lang/StringBuilder;

    invoke-virtual {v3}, Ljava/lang/StringBuilder;->length()I

    move-result v3

    iget v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->maxLength:I

    if-ge v3, v4, :cond_94

    .line 490
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->primary:Ljava/lang/StringBuilder;

    invoke-virtual {v3, v15}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    .line 492
    :cond_94
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->alternate:Ljava/lang/StringBuilder;

    invoke-virtual {v3}, Ljava/lang/StringBuilder;->length()I

    move-result v3

    iget v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->maxLength:I

    if-ge v3, v4, :cond_110

    .line 493
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->alternate:Ljava/lang/StringBuilder;

    invoke-virtual {v3, v15}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    goto/16 :goto_54

    .line 497
    :cond_95
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->primary:Ljava/lang/StringBuilder;

    invoke-virtual {v3}, Ljava/lang/StringBuilder;->length()I

    move-result v3

    iget v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->maxLength:I

    if-ge v3, v4, :cond_96

    .line 498
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->primary:Ljava/lang/StringBuilder;

    const/16 v4, 0x4b

    invoke-virtual {v3, v4}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    goto :goto_2f

    :cond_96
    const/16 v4, 0x4b

    .line 500
    :goto_2f
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->alternate:Ljava/lang/StringBuilder;

    invoke-virtual {v3}, Ljava/lang/StringBuilder;->length()I

    move-result v3

    iget v9, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->maxLength:I

    if-ge v3, v9, :cond_110

    .line 501
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->alternate:Ljava/lang/StringBuilder;

    invoke-virtual {v3, v4}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    goto/16 :goto_54

    :cond_97
    if-le v7, v6, :cond_98

    add-int/lit8 v3, v7, -0x2

    const-string v9, "B"

    const-string v10, "H"

    const-string v11, "D"

    .line 506
    filled-new-array {v9, v10, v11}, [Ljava/lang/String;

    move-result-object v9

    invoke-static {v1, v3, v6, v9}, Lcom/helpshift/support/external/DoubleMetaphone;->contains(Ljava/lang/String;II[Ljava/lang/String;)Z

    move-result v3

    if-nez v3, :cond_9a

    :cond_98
    if-le v7, v14, :cond_99

    add-int/lit8 v3, v7, -0x3

    const-string v9, "B"

    const-string v10, "H"

    const-string v11, "D"

    filled-new-array {v9, v10, v11}, [Ljava/lang/String;

    move-result-object v9

    .line 507
    invoke-static {v1, v3, v6, v9}, Lcom/helpshift/support/external/DoubleMetaphone;->contains(Ljava/lang/String;II[Ljava/lang/String;)Z

    move-result v3

    if-nez v3, :cond_9a

    :cond_99
    if-le v7, v4, :cond_9b

    add-int/lit8 v3, v7, -0x4

    const-string v4, "B"

    const-string v9, "H"

    filled-new-array {v4, v9}, [Ljava/lang/String;

    move-result-object v4

    .line 508
    invoke-static {v1, v3, v6, v4}, Lcom/helpshift/support/external/DoubleMetaphone;->contains(Ljava/lang/String;II[Ljava/lang/String;)Z

    move-result v3

    if-eqz v3, :cond_9b

    :cond_9a
    add-int/lit8 v7, v7, 0x2

    goto/16 :goto_54

    :cond_9b
    if-le v7, v14, :cond_9d

    add-int/lit8 v3, v7, -0x1

    .line 513
    invoke-virtual {v0, v1, v3}, Lcom/helpshift/support/external/DoubleMetaphone;->charAt(Ljava/lang/String;I)C

    move-result v3

    const/16 v4, 0x55

    if-ne v3, v4, :cond_9d

    add-int/lit8 v3, v7, -0x3

    const-string v4, "C"

    const-string v9, "G"

    const-string v10, "L"

    const-string v11, "R"

    const-string v12, "T"

    filled-new-array {v4, v9, v10, v11, v12}, [Ljava/lang/String;

    move-result-object v4

    .line 514
    invoke-static {v1, v3, v6, v4}, Lcom/helpshift/support/external/DoubleMetaphone;->contains(Ljava/lang/String;II[Ljava/lang/String;)Z

    move-result v3

    if-eqz v3, :cond_9d

    .line 518
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->primary:Ljava/lang/StringBuilder;

    invoke-virtual {v3}, Ljava/lang/StringBuilder;->length()I

    move-result v3

    iget v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->maxLength:I

    if-ge v3, v4, :cond_9c

    .line 519
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->primary:Ljava/lang/StringBuilder;

    const/16 v4, 0x46

    invoke-virtual {v3, v4}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    goto :goto_30

    :cond_9c
    const/16 v4, 0x46

    .line 521
    :goto_30
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->alternate:Ljava/lang/StringBuilder;

    invoke-virtual {v3}, Ljava/lang/StringBuilder;->length()I

    move-result v3

    iget v9, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->maxLength:I

    if-ge v3, v9, :cond_9f

    .line 522
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->alternate:Ljava/lang/StringBuilder;

    invoke-virtual {v3, v4}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    goto :goto_32

    :cond_9d
    if-lez v7, :cond_9f

    add-int/lit8 v3, v7, -0x1

    .line 525
    invoke-virtual {v0, v1, v3}, Lcom/helpshift/support/external/DoubleMetaphone;->charAt(Ljava/lang/String;I)C

    move-result v3

    const/16 v4, 0x49

    if-eq v3, v4, :cond_9f

    .line 526
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->primary:Ljava/lang/StringBuilder;

    invoke-virtual {v3}, Ljava/lang/StringBuilder;->length()I

    move-result v3

    iget v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->maxLength:I

    if-ge v3, v4, :cond_9e

    .line 527
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->primary:Ljava/lang/StringBuilder;

    const/16 v4, 0x4b

    invoke-virtual {v3, v4}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    goto :goto_31

    :cond_9e
    const/16 v4, 0x4b

    .line 529
    :goto_31
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->alternate:Ljava/lang/StringBuilder;

    invoke-virtual {v3}, Ljava/lang/StringBuilder;->length()I

    move-result v3

    iget v9, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->maxLength:I

    if-ge v3, v9, :cond_9f

    .line 530
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->alternate:Ljava/lang/StringBuilder;

    invoke-virtual {v3, v4}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    :cond_9f
    :goto_32
    add-int/lit8 v7, v7, 0x2

    goto/16 :goto_54

    .line 537
    :cond_a0
    invoke-virtual {v0, v1, v3}, Lcom/helpshift/support/external/DoubleMetaphone;->charAt(Ljava/lang/String;I)C

    move-result v9

    const/16 v10, 0x4e

    if-ne v9, v10, :cond_a9

    if-ne v7, v6, :cond_a3

    const-string v4, "AEIOUY"

    .line 538
    invoke-virtual {v0, v1, v5}, Lcom/helpshift/support/external/DoubleMetaphone;->charAt(Ljava/lang/String;I)C

    move-result v9

    invoke-virtual {v4, v9}, Ljava/lang/String;->indexOf(I)I

    move-result v4

    const/4 v9, -0x1

    if-eq v4, v9, :cond_a3

    if-nez v2, :cond_a3

    .line 539
    iget v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->maxLength:I

    iget-object v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->primary:Ljava/lang/StringBuilder;

    invoke-virtual {v4}, Ljava/lang/StringBuilder;->length()I

    move-result v4

    sub-int/2addr v3, v4

    const-string v4, "KN"

    .line 540
    invoke-virtual {v4}, Ljava/lang/String;->length()I

    move-result v4

    if-gt v4, v3, :cond_a1

    .line 541
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->primary:Ljava/lang/StringBuilder;

    const-string v4, "KN"

    invoke-virtual {v3, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    goto :goto_33

    .line 544
    :cond_a1
    iget-object v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->primary:Ljava/lang/StringBuilder;

    const-string v9, "KN"

    invoke-virtual {v9, v5, v3}, Ljava/lang/String;->substring(II)Ljava/lang/String;

    move-result-object v3

    invoke-virtual {v4, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    .line 546
    :goto_33
    iget v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->maxLength:I

    iget-object v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->alternate:Ljava/lang/StringBuilder;

    invoke-virtual {v4}, Ljava/lang/StringBuilder;->length()I

    move-result v4

    sub-int/2addr v3, v4

    const-string v4, "N"

    .line 547
    invoke-virtual {v4}, Ljava/lang/String;->length()I

    move-result v4

    if-gt v4, v3, :cond_a2

    .line 548
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->alternate:Ljava/lang/StringBuilder;

    const-string v4, "N"

    invoke-virtual {v3, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    goto/16 :goto_36

    .line 551
    :cond_a2
    iget-object v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->alternate:Ljava/lang/StringBuilder;

    const-string v9, "N"

    invoke-virtual {v9, v5, v3}, Ljava/lang/String;->substring(II)Ljava/lang/String;

    move-result-object v3

    invoke-virtual {v4, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    goto/16 :goto_36

    :cond_a3
    add-int/lit8 v4, v7, 0x2

    const-string v9, "EY"

    .line 554
    filled-new-array {v9}, [Ljava/lang/String;

    move-result-object v9

    invoke-static {v1, v4, v14, v9}, Lcom/helpshift/support/external/DoubleMetaphone;->contains(Ljava/lang/String;II[Ljava/lang/String;)Z

    move-result v4

    if-nez v4, :cond_a6

    .line 555
    invoke-virtual {v0, v1, v3}, Lcom/helpshift/support/external/DoubleMetaphone;->charAt(Ljava/lang/String;I)C

    move-result v3

    const/16 v4, 0x59

    if-eq v3, v4, :cond_a6

    if-nez v2, :cond_a6

    .line 556
    iget v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->maxLength:I

    iget-object v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->primary:Ljava/lang/StringBuilder;

    invoke-virtual {v4}, Ljava/lang/StringBuilder;->length()I

    move-result v4

    sub-int/2addr v3, v4

    const-string v4, "N"

    .line 557
    invoke-virtual {v4}, Ljava/lang/String;->length()I

    move-result v4

    if-gt v4, v3, :cond_a4

    .line 558
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->primary:Ljava/lang/StringBuilder;

    const-string v4, "N"

    invoke-virtual {v3, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    goto :goto_34

    .line 561
    :cond_a4
    iget-object v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->primary:Ljava/lang/StringBuilder;

    const-string v9, "N"

    invoke-virtual {v9, v5, v3}, Ljava/lang/String;->substring(II)Ljava/lang/String;

    move-result-object v3

    invoke-virtual {v4, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    .line 563
    :goto_34
    iget v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->maxLength:I

    iget-object v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->alternate:Ljava/lang/StringBuilder;

    invoke-virtual {v4}, Ljava/lang/StringBuilder;->length()I

    move-result v4

    sub-int/2addr v3, v4

    const-string v4, "KN"

    .line 564
    invoke-virtual {v4}, Ljava/lang/String;->length()I

    move-result v4

    if-gt v4, v3, :cond_a5

    .line 565
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->alternate:Ljava/lang/StringBuilder;

    const-string v4, "KN"

    invoke-virtual {v3, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    goto :goto_36

    .line 568
    :cond_a5
    iget-object v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->alternate:Ljava/lang/StringBuilder;

    const-string v9, "KN"

    invoke-virtual {v9, v5, v3}, Ljava/lang/String;->substring(II)Ljava/lang/String;

    move-result-object v3

    invoke-virtual {v4, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    goto :goto_36

    .line 572
    :cond_a6
    iget v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->maxLength:I

    iget-object v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->primary:Ljava/lang/StringBuilder;

    invoke-virtual {v4}, Ljava/lang/StringBuilder;->length()I

    move-result v4

    sub-int/2addr v3, v4

    const-string v4, "KN"

    .line 573
    invoke-virtual {v4}, Ljava/lang/String;->length()I

    move-result v4

    if-gt v4, v3, :cond_a7

    .line 574
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->primary:Ljava/lang/StringBuilder;

    const-string v4, "KN"

    invoke-virtual {v3, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    goto :goto_35

    .line 577
    :cond_a7
    iget-object v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->primary:Ljava/lang/StringBuilder;

    const-string v9, "KN"

    invoke-virtual {v9, v5, v3}, Ljava/lang/String;->substring(II)Ljava/lang/String;

    move-result-object v3

    invoke-virtual {v4, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    .line 579
    :goto_35
    iget v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->maxLength:I

    iget-object v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->alternate:Ljava/lang/StringBuilder;

    invoke-virtual {v4}, Ljava/lang/StringBuilder;->length()I

    move-result v4

    sub-int/2addr v3, v4

    const-string v4, "KN"

    .line 580
    invoke-virtual {v4}, Ljava/lang/String;->length()I

    move-result v4

    if-gt v4, v3, :cond_a8

    .line 581
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->alternate:Ljava/lang/StringBuilder;

    const-string v4, "KN"

    invoke-virtual {v3, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    goto :goto_36

    .line 584
    :cond_a8
    iget-object v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->alternate:Ljava/lang/StringBuilder;

    const-string v9, "KN"

    invoke-virtual {v9, v5, v3}, Ljava/lang/String;->substring(II)Ljava/lang/String;

    move-result-object v3

    invoke-virtual {v4, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    :goto_36
    add-int/lit8 v7, v7, 0x2

    goto/16 :goto_54

    :cond_a9
    const-string v9, "LI"

    .line 589
    filled-new-array {v9}, [Ljava/lang/String;

    move-result-object v9

    invoke-static {v1, v3, v14, v9}, Lcom/helpshift/support/external/DoubleMetaphone;->contains(Ljava/lang/String;II[Ljava/lang/String;)Z

    move-result v9

    if-eqz v9, :cond_ac

    if-nez v2, :cond_ac

    .line 590
    iget v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->maxLength:I

    iget-object v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->primary:Ljava/lang/StringBuilder;

    invoke-virtual {v4}, Ljava/lang/StringBuilder;->length()I

    move-result v4

    sub-int/2addr v3, v4

    const-string v4, "KL"

    .line 591
    invoke-virtual {v4}, Ljava/lang/String;->length()I

    move-result v4

    if-gt v4, v3, :cond_aa

    .line 592
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->primary:Ljava/lang/StringBuilder;

    const-string v4, "KL"

    invoke-virtual {v3, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    goto :goto_37

    .line 595
    :cond_aa
    iget-object v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->primary:Ljava/lang/StringBuilder;

    const-string v9, "KL"

    invoke-virtual {v9, v5, v3}, Ljava/lang/String;->substring(II)Ljava/lang/String;

    move-result-object v3

    invoke-virtual {v4, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    .line 597
    :goto_37
    iget v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->maxLength:I

    iget-object v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->alternate:Ljava/lang/StringBuilder;

    invoke-virtual {v4}, Ljava/lang/StringBuilder;->length()I

    move-result v4

    sub-int/2addr v3, v4

    const-string v4, "L"

    .line 598
    invoke-virtual {v4}, Ljava/lang/String;->length()I

    move-result v4

    if-gt v4, v3, :cond_ab

    .line 599
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->alternate:Ljava/lang/StringBuilder;

    const-string v4, "L"

    invoke-virtual {v3, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    goto :goto_38

    .line 602
    :cond_ab
    iget-object v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->alternate:Ljava/lang/StringBuilder;

    const-string v9, "L"

    invoke-virtual {v9, v5, v3}, Ljava/lang/String;->substring(II)Ljava/lang/String;

    move-result-object v3

    invoke-virtual {v4, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    :goto_38
    add-int/lit8 v7, v7, 0x2

    goto/16 :goto_54

    :cond_ac
    if-nez v7, :cond_b0

    .line 607
    invoke-virtual {v0, v1, v3}, Lcom/helpshift/support/external/DoubleMetaphone;->charAt(Ljava/lang/String;I)C

    move-result v9

    const/16 v10, 0x59

    if-eq v9, v10, :cond_ad

    sget-object v9, Lcom/helpshift/support/external/DoubleMetaphone;->ES_EP_EB_EL_EY_IB_IL_IN_IE_EI_ER:[Ljava/lang/String;

    .line 608
    invoke-static {v1, v3, v14, v9}, Lcom/helpshift/support/external/DoubleMetaphone;->contains(Ljava/lang/String;II[Ljava/lang/String;)Z

    move-result v9

    if-eqz v9, :cond_b0

    .line 610
    :cond_ad
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->primary:Ljava/lang/StringBuilder;

    invoke-virtual {v3}, Ljava/lang/StringBuilder;->length()I

    move-result v3

    iget v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->maxLength:I

    if-ge v3, v4, :cond_ae

    .line 611
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->primary:Ljava/lang/StringBuilder;

    const/16 v4, 0x4b

    invoke-virtual {v3, v4}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    .line 613
    :cond_ae
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->alternate:Ljava/lang/StringBuilder;

    invoke-virtual {v3}, Ljava/lang/StringBuilder;->length()I

    move-result v3

    iget v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->maxLength:I

    if-ge v3, v4, :cond_af

    .line 614
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->alternate:Ljava/lang/StringBuilder;

    invoke-virtual {v3, v15}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    :cond_af
    add-int/lit8 v7, v7, 0x2

    goto/16 :goto_54

    :cond_b0
    const-string v9, "ER"

    .line 618
    filled-new-array {v9}, [Ljava/lang/String;

    move-result-object v9

    invoke-static {v1, v3, v14, v9}, Lcom/helpshift/support/external/DoubleMetaphone;->contains(Ljava/lang/String;II[Ljava/lang/String;)Z

    move-result v9

    if-nez v9, :cond_b1

    .line 619
    invoke-virtual {v0, v1, v3}, Lcom/helpshift/support/external/DoubleMetaphone;->charAt(Ljava/lang/String;I)C

    move-result v9

    const/16 v10, 0x59

    if-ne v9, v10, :cond_b4

    :cond_b1
    const/4 v9, 0x6

    const-string v10, "DANGER"

    const-string v11, "RANGER"

    const-string v12, "MANGER"

    filled-new-array {v10, v11, v12}, [Ljava/lang/String;

    move-result-object v10

    .line 620
    invoke-static {v1, v5, v9, v10}, Lcom/helpshift/support/external/DoubleMetaphone;->contains(Ljava/lang/String;II[Ljava/lang/String;)Z

    move-result v9

    if-nez v9, :cond_b4

    add-int/lit8 v9, v7, -0x1

    const-string v10, "E"

    const-string v11, "I"

    filled-new-array {v10, v11}, [Ljava/lang/String;

    move-result-object v10

    .line 621
    invoke-static {v1, v9, v6, v10}, Lcom/helpshift/support/external/DoubleMetaphone;->contains(Ljava/lang/String;II[Ljava/lang/String;)Z

    move-result v10

    if-nez v10, :cond_b4

    const-string v10, "RGY"

    const-string v11, "OGY"

    filled-new-array {v10, v11}, [Ljava/lang/String;

    move-result-object v10

    .line 622
    invoke-static {v1, v9, v4, v10}, Lcom/helpshift/support/external/DoubleMetaphone;->contains(Ljava/lang/String;II[Ljava/lang/String;)Z

    move-result v9

    if-nez v9, :cond_b4

    .line 624
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->primary:Ljava/lang/StringBuilder;

    invoke-virtual {v3}, Ljava/lang/StringBuilder;->length()I

    move-result v3

    iget v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->maxLength:I

    if-ge v3, v4, :cond_b2

    .line 625
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->primary:Ljava/lang/StringBuilder;

    const/16 v4, 0x4b

    invoke-virtual {v3, v4}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    .line 627
    :cond_b2
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->alternate:Ljava/lang/StringBuilder;

    invoke-virtual {v3}, Ljava/lang/StringBuilder;->length()I

    move-result v3

    iget v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->maxLength:I

    if-ge v3, v4, :cond_b3

    .line 628
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->alternate:Ljava/lang/StringBuilder;

    invoke-virtual {v3, v15}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    :cond_b3
    add-int/lit8 v7, v7, 0x2

    goto/16 :goto_54

    :cond_b4
    const-string v9, "E"

    const-string v10, "I"

    const-string v11, "Y"

    .line 632
    filled-new-array {v9, v10, v11}, [Ljava/lang/String;

    move-result-object v9

    invoke-static {v1, v3, v6, v9}, Lcom/helpshift/support/external/DoubleMetaphone;->contains(Ljava/lang/String;II[Ljava/lang/String;)Z

    move-result v9

    if-nez v9, :cond_b9

    add-int/lit8 v9, v7, -0x1

    const-string v10, "AGGI"

    const-string v11, "OGGI"

    filled-new-array {v10, v11}, [Ljava/lang/String;

    move-result-object v10

    const/4 v11, 0x4

    .line 633
    invoke-static {v1, v9, v11, v10}, Lcom/helpshift/support/external/DoubleMetaphone;->contains(Ljava/lang/String;II[Ljava/lang/String;)Z

    move-result v9

    if-eqz v9, :cond_b5

    goto :goto_3a

    .line 664
    :cond_b5
    invoke-virtual {v0, v1, v3}, Lcom/helpshift/support/external/DoubleMetaphone;->charAt(Ljava/lang/String;I)C

    move-result v4

    const/16 v9, 0x47

    if-ne v4, v9, :cond_b7

    add-int/lit8 v7, v7, 0x2

    .line 666
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->primary:Ljava/lang/StringBuilder;

    invoke-virtual {v3}, Ljava/lang/StringBuilder;->length()I

    move-result v3

    iget v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->maxLength:I

    if-ge v3, v4, :cond_b6

    .line 667
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->primary:Ljava/lang/StringBuilder;

    const/16 v4, 0x4b

    invoke-virtual {v3, v4}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    goto :goto_39

    :cond_b6
    const/16 v4, 0x4b

    .line 669
    :goto_39
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->alternate:Ljava/lang/StringBuilder;

    invoke-virtual {v3}, Ljava/lang/StringBuilder;->length()I

    move-result v3

    iget v9, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->maxLength:I

    if-ge v3, v9, :cond_110

    .line 670
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->alternate:Ljava/lang/StringBuilder;

    invoke-virtual {v3, v4}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    goto/16 :goto_54

    :cond_b7
    const/16 v4, 0x4b

    .line 675
    iget-object v7, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->primary:Ljava/lang/StringBuilder;

    invoke-virtual {v7}, Ljava/lang/StringBuilder;->length()I

    move-result v7

    iget v9, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->maxLength:I

    if-ge v7, v9, :cond_b8

    .line 676
    iget-object v7, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->primary:Ljava/lang/StringBuilder;

    invoke-virtual {v7, v4}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    .line 678
    :cond_b8
    iget-object v7, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->alternate:Ljava/lang/StringBuilder;

    invoke-virtual {v7}, Ljava/lang/StringBuilder;->length()I

    move-result v7

    iget v9, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->maxLength:I

    if-ge v7, v9, :cond_47

    .line 679
    iget-object v7, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->alternate:Ljava/lang/StringBuilder;

    invoke-virtual {v7, v4}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    goto/16 :goto_17

    :cond_b9
    :goto_3a
    const-string v9, "VAN "

    const-string v10, "VON "

    .line 635
    filled-new-array {v9, v10}, [Ljava/lang/String;

    move-result-object v9

    const/4 v10, 0x4

    invoke-static {v1, v5, v10, v9}, Lcom/helpshift/support/external/DoubleMetaphone;->contains(Ljava/lang/String;II[Ljava/lang/String;)Z

    move-result v9

    if-nez v9, :cond_be

    const-string v9, "SCH"

    filled-new-array {v9}, [Ljava/lang/String;

    move-result-object v9

    .line 636
    invoke-static {v1, v5, v4, v9}, Lcom/helpshift/support/external/DoubleMetaphone;->contains(Ljava/lang/String;II[Ljava/lang/String;)Z

    move-result v9

    if-nez v9, :cond_be

    const-string v9, "ET"

    filled-new-array {v9}, [Ljava/lang/String;

    move-result-object v9

    .line 637
    invoke-static {v1, v3, v14, v9}, Lcom/helpshift/support/external/DoubleMetaphone;->contains(Ljava/lang/String;II[Ljava/lang/String;)Z

    move-result v9

    if-eqz v9, :cond_ba

    goto :goto_3b

    :cond_ba
    const-string v9, "IER"

    .line 646
    filled-new-array {v9}, [Ljava/lang/String;

    move-result-object v9

    invoke-static {v1, v3, v4, v9}, Lcom/helpshift/support/external/DoubleMetaphone;->contains(Ljava/lang/String;II[Ljava/lang/String;)Z

    move-result v3

    if-eqz v3, :cond_bc

    .line 647
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->primary:Ljava/lang/StringBuilder;

    invoke-virtual {v3}, Ljava/lang/StringBuilder;->length()I

    move-result v3

    iget v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->maxLength:I

    if-ge v3, v4, :cond_bb

    .line 648
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->primary:Ljava/lang/StringBuilder;

    invoke-virtual {v3, v15}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    .line 650
    :cond_bb
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->alternate:Ljava/lang/StringBuilder;

    invoke-virtual {v3}, Ljava/lang/StringBuilder;->length()I

    move-result v3

    iget v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->maxLength:I

    if-ge v3, v4, :cond_c0

    .line 651
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->alternate:Ljava/lang/StringBuilder;

    invoke-virtual {v3, v15}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    goto :goto_3c

    .line 655
    :cond_bc
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->primary:Ljava/lang/StringBuilder;

    invoke-virtual {v3}, Ljava/lang/StringBuilder;->length()I

    move-result v3

    iget v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->maxLength:I

    if-ge v3, v4, :cond_bd

    .line 656
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->primary:Ljava/lang/StringBuilder;

    invoke-virtual {v3, v15}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    .line 658
    :cond_bd
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->alternate:Ljava/lang/StringBuilder;

    invoke-virtual {v3}, Ljava/lang/StringBuilder;->length()I

    move-result v3

    iget v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->maxLength:I

    if-ge v3, v4, :cond_c0

    .line 659
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->alternate:Ljava/lang/StringBuilder;

    const/16 v4, 0x4b

    invoke-virtual {v3, v4}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    goto :goto_3c

    :cond_be
    :goto_3b
    const/16 v4, 0x4b

    .line 639
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->primary:Ljava/lang/StringBuilder;

    invoke-virtual {v3}, Ljava/lang/StringBuilder;->length()I

    move-result v3

    iget v9, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->maxLength:I

    if-ge v3, v9, :cond_bf

    .line 640
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->primary:Ljava/lang/StringBuilder;

    invoke-virtual {v3, v4}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    .line 642
    :cond_bf
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->alternate:Ljava/lang/StringBuilder;

    invoke-virtual {v3}, Ljava/lang/StringBuilder;->length()I

    move-result v3

    iget v9, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->maxLength:I

    if-ge v3, v9, :cond_c0

    .line 643
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->alternate:Ljava/lang/StringBuilder;

    invoke-virtual {v3, v4}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    :cond_c0
    :goto_3c
    add-int/lit8 v7, v7, 0x2

    goto/16 :goto_54

    .line 466
    :pswitch_10
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->primary:Ljava/lang/StringBuilder;

    invoke-virtual {v3}, Ljava/lang/StringBuilder;->length()I

    move-result v3

    iget v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->maxLength:I

    if-ge v3, v4, :cond_c1

    .line 467
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->primary:Ljava/lang/StringBuilder;

    const/16 v4, 0x46

    invoke-virtual {v3, v4}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    goto :goto_3d

    :cond_c1
    const/16 v4, 0x46

    .line 469
    :goto_3d
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->alternate:Ljava/lang/StringBuilder;

    invoke-virtual {v3}, Ljava/lang/StringBuilder;->length()I

    move-result v3

    iget v9, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->maxLength:I

    if-ge v3, v9, :cond_c2

    .line 470
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->alternate:Ljava/lang/StringBuilder;

    invoke-virtual {v3, v4}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    :cond_c2
    add-int/lit8 v3, v7, 0x1

    .line 472
    invoke-virtual {v0, v1, v3}, Lcom/helpshift/support/external/DoubleMetaphone;->charAt(Ljava/lang/String;I)C

    move-result v9

    if-ne v9, v4, :cond_47

    add-int/lit8 v7, v7, 0x2

    goto/16 :goto_54

    :pswitch_11
    const-string v3, "DG"

    .line 415
    filled-new-array {v3}, [Ljava/lang/String;

    move-result-object v3

    invoke-static {v1, v7, v14, v3}, Lcom/helpshift/support/external/DoubleMetaphone;->contains(Ljava/lang/String;II[Ljava/lang/String;)Z

    move-result v3

    if-eqz v3, :cond_c8

    add-int/lit8 v3, v7, 0x2

    const-string v4, "I"

    const-string v9, "E"

    const-string v10, "Y"

    .line 417
    filled-new-array {v4, v9, v10}, [Ljava/lang/String;

    move-result-object v4

    invoke-static {v1, v3, v6, v4}, Lcom/helpshift/support/external/DoubleMetaphone;->contains(Ljava/lang/String;II[Ljava/lang/String;)Z

    move-result v4

    if-eqz v4, :cond_c5

    .line 418
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->primary:Ljava/lang/StringBuilder;

    invoke-virtual {v3}, Ljava/lang/StringBuilder;->length()I

    move-result v3

    iget v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->maxLength:I

    if-ge v3, v4, :cond_c3

    .line 419
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->primary:Ljava/lang/StringBuilder;

    invoke-virtual {v3, v15}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    .line 421
    :cond_c3
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->alternate:Ljava/lang/StringBuilder;

    invoke-virtual {v3}, Ljava/lang/StringBuilder;->length()I

    move-result v3

    iget v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->maxLength:I

    if-ge v3, v4, :cond_c4

    .line 422
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->alternate:Ljava/lang/StringBuilder;

    invoke-virtual {v3, v15}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    :cond_c4
    add-int/lit8 v7, v7, 0x3

    goto/16 :goto_54

    .line 428
    :cond_c5
    iget v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->maxLength:I

    iget-object v7, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->primary:Ljava/lang/StringBuilder;

    invoke-virtual {v7}, Ljava/lang/StringBuilder;->length()I

    move-result v7

    sub-int/2addr v4, v7

    const-string v7, "TK"

    .line 429
    invoke-virtual {v7}, Ljava/lang/String;->length()I

    move-result v7

    if-gt v7, v4, :cond_c6

    .line 430
    iget-object v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->primary:Ljava/lang/StringBuilder;

    const-string v7, "TK"

    invoke-virtual {v4, v7}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    goto :goto_3e

    .line 433
    :cond_c6
    iget-object v7, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->primary:Ljava/lang/StringBuilder;

    const-string v9, "TK"

    invoke-virtual {v9, v5, v4}, Ljava/lang/String;->substring(II)Ljava/lang/String;

    move-result-object v4

    invoke-virtual {v7, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    .line 435
    :goto_3e
    iget v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->maxLength:I

    iget-object v7, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->alternate:Ljava/lang/StringBuilder;

    invoke-virtual {v7}, Ljava/lang/StringBuilder;->length()I

    move-result v7

    sub-int/2addr v4, v7

    const-string v7, "TK"

    .line 436
    invoke-virtual {v7}, Ljava/lang/String;->length()I

    move-result v7

    if-gt v7, v4, :cond_c7

    .line 437
    iget-object v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->alternate:Ljava/lang/StringBuilder;

    const-string v7, "TK"

    invoke-virtual {v4, v7}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    goto/16 :goto_17

    .line 440
    :cond_c7
    iget-object v7, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->alternate:Ljava/lang/StringBuilder;

    const-string v9, "TK"

    invoke-virtual {v9, v5, v4}, Ljava/lang/String;->substring(II)Ljava/lang/String;

    move-result-object v4

    invoke-virtual {v7, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    goto/16 :goto_17

    :cond_c8
    const-string v3, "DT"

    const-string v4, "DD"

    .line 445
    filled-new-array {v3, v4}, [Ljava/lang/String;

    move-result-object v3

    invoke-static {v1, v7, v14, v3}, Lcom/helpshift/support/external/DoubleMetaphone;->contains(Ljava/lang/String;II[Ljava/lang/String;)Z

    move-result v3

    if-eqz v3, :cond_cb

    .line 446
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->primary:Ljava/lang/StringBuilder;

    invoke-virtual {v3}, Ljava/lang/StringBuilder;->length()I

    move-result v3

    iget v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->maxLength:I

    if-ge v3, v4, :cond_c9

    .line 447
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->primary:Ljava/lang/StringBuilder;

    invoke-virtual {v3, v12}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    .line 449
    :cond_c9
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->alternate:Ljava/lang/StringBuilder;

    invoke-virtual {v3}, Ljava/lang/StringBuilder;->length()I

    move-result v3

    iget v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->maxLength:I

    if-ge v3, v4, :cond_ca

    .line 450
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->alternate:Ljava/lang/StringBuilder;

    invoke-virtual {v3, v12}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    :cond_ca
    add-int/lit8 v7, v7, 0x2

    goto/16 :goto_54

    .line 455
    :cond_cb
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->primary:Ljava/lang/StringBuilder;

    invoke-virtual {v3}, Ljava/lang/StringBuilder;->length()I

    move-result v3

    iget v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->maxLength:I

    if-ge v3, v4, :cond_cc

    .line 456
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->primary:Ljava/lang/StringBuilder;

    invoke-virtual {v3, v12}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    .line 458
    :cond_cc
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->alternate:Ljava/lang/StringBuilder;

    invoke-virtual {v3}, Ljava/lang/StringBuilder;->length()I

    move-result v3

    iget v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->maxLength:I

    if-ge v3, v4, :cond_cd

    .line 459
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->alternate:Ljava/lang/StringBuilder;

    invoke-virtual {v3, v12}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    :cond_cd
    add-int/lit8 v7, v7, 0x1

    goto/16 :goto_54

    :pswitch_12
    const-string v3, "CHIA"

    .line 167
    filled-new-array {v3}, [Ljava/lang/String;

    move-result-object v3

    const/4 v9, 0x4

    invoke-static {v1, v7, v9, v3}, Lcom/helpshift/support/external/DoubleMetaphone;->contains(Ljava/lang/String;II[Ljava/lang/String;)Z

    move-result v3

    if-eqz v3, :cond_ce

    const/4 v3, 0x1

    :goto_3f
    const/4 v12, -0x1

    goto :goto_41

    :cond_ce
    if-gt v7, v6, :cond_cf

    const/4 v3, 0x0

    goto :goto_3f

    :cond_cf
    const-string v3, "AEIOUY"

    add-int/lit8 v9, v7, -0x2

    .line 173
    invoke-virtual {v0, v1, v9}, Lcom/helpshift/support/external/DoubleMetaphone;->charAt(Ljava/lang/String;I)C

    move-result v12

    invoke-virtual {v3, v12}, Ljava/lang/String;->indexOf(I)I

    move-result v3

    const/4 v12, -0x1

    if-eq v3, v12, :cond_d1

    :cond_d0
    :goto_40
    const/4 v3, 0x0

    goto :goto_41

    :cond_d1
    add-int/lit8 v3, v7, -0x1

    const-string v13, "ACH"

    .line 176
    filled-new-array {v13}, [Ljava/lang/String;

    move-result-object v13

    invoke-static {v1, v3, v4, v13}, Lcom/helpshift/support/external/DoubleMetaphone;->contains(Ljava/lang/String;II[Ljava/lang/String;)Z

    move-result v3

    if-nez v3, :cond_d2

    goto :goto_40

    :cond_d2
    add-int/lit8 v3, v7, 0x2

    .line 180
    invoke-virtual {v0, v1, v3}, Lcom/helpshift/support/external/DoubleMetaphone;->charAt(Ljava/lang/String;I)C

    move-result v3

    const/16 v13, 0x49

    if-eq v3, v13, :cond_d3

    const/16 v13, 0x45

    if-ne v3, v13, :cond_d4

    :cond_d3
    const/4 v3, 0x6

    const-string v13, "BACHER"

    const-string v15, "MACHER"

    .line 181
    filled-new-array {v13, v15}, [Ljava/lang/String;

    move-result-object v13

    .line 182
    invoke-static {v1, v9, v3, v13}, Lcom/helpshift/support/external/DoubleMetaphone;->contains(Ljava/lang/String;II[Ljava/lang/String;)Z

    move-result v3

    if-eqz v3, :cond_d0

    :cond_d4
    const/4 v3, 0x1

    :goto_41
    if-eqz v3, :cond_d7

    .line 185
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->primary:Ljava/lang/StringBuilder;

    invoke-virtual {v3}, Ljava/lang/StringBuilder;->length()I

    move-result v3

    iget v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->maxLength:I

    if-ge v3, v4, :cond_d5

    .line 186
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->primary:Ljava/lang/StringBuilder;

    const/16 v4, 0x4b

    invoke-virtual {v3, v4}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    goto :goto_42

    :cond_d5
    const/16 v4, 0x4b

    .line 188
    :goto_42
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->alternate:Ljava/lang/StringBuilder;

    invoke-virtual {v3}, Ljava/lang/StringBuilder;->length()I

    move-result v3

    iget v9, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->maxLength:I

    if-ge v3, v9, :cond_d6

    .line 189
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->alternate:Ljava/lang/StringBuilder;

    invoke-virtual {v3, v4}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    :cond_d6
    add-int/lit8 v7, v7, 0x2

    :goto_43
    const/16 v9, 0x4b

    goto/16 :goto_54

    :cond_d7
    if-nez v7, :cond_da

    const/4 v3, 0x6

    const-string v9, "CAESAR"

    .line 194
    filled-new-array {v9}, [Ljava/lang/String;

    move-result-object v9

    invoke-static {v1, v7, v3, v9}, Lcom/helpshift/support/external/DoubleMetaphone;->contains(Ljava/lang/String;II[Ljava/lang/String;)Z

    move-result v3

    if-eqz v3, :cond_da

    .line 195
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->primary:Ljava/lang/StringBuilder;

    invoke-virtual {v3}, Ljava/lang/StringBuilder;->length()I

    move-result v3

    iget v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->maxLength:I

    if-ge v3, v4, :cond_d8

    .line 196
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->primary:Ljava/lang/StringBuilder;

    invoke-virtual {v3, v11}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    .line 198
    :cond_d8
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->alternate:Ljava/lang/StringBuilder;

    invoke-virtual {v3}, Ljava/lang/StringBuilder;->length()I

    move-result v3

    iget v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->maxLength:I

    if-ge v3, v4, :cond_d9

    .line 199
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->alternate:Ljava/lang/StringBuilder;

    invoke-virtual {v3, v11}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    :cond_d9
    add-int/lit8 v7, v7, 0x2

    goto :goto_43

    :cond_da
    const-string v3, "CH"

    .line 204
    filled-new-array {v3}, [Ljava/lang/String;

    move-result-object v3

    invoke-static {v1, v7, v14, v3}, Lcom/helpshift/support/external/DoubleMetaphone;->contains(Ljava/lang/String;II[Ljava/lang/String;)Z

    move-result v3

    if-eqz v3, :cond_ee

    if-eqz v7, :cond_db

    :goto_44
    const/4 v3, 0x0

    goto :goto_45

    :cond_db
    add-int/lit8 v3, v7, 0x1

    const/4 v9, 0x5

    const-string v10, "HARAC"

    const-string v11, "HARIS"

    .line 210
    filled-new-array {v10, v11}, [Ljava/lang/String;

    move-result-object v10

    invoke-static {v1, v3, v9, v10}, Lcom/helpshift/support/external/DoubleMetaphone;->contains(Ljava/lang/String;II[Ljava/lang/String;)Z

    move-result v9

    if-nez v9, :cond_dc

    const-string v9, "HOR"

    const-string v10, "HYM"

    const-string v11, "HIA"

    const-string v13, "HEM"

    filled-new-array {v9, v10, v11, v13}, [Ljava/lang/String;

    move-result-object v9

    .line 211
    invoke-static {v1, v3, v4, v9}, Lcom/helpshift/support/external/DoubleMetaphone;->contains(Ljava/lang/String;II[Ljava/lang/String;)Z

    move-result v3

    if-nez v3, :cond_dc

    goto :goto_44

    :cond_dc
    const/4 v3, 0x5

    const-string v9, "CHORE"

    .line 215
    filled-new-array {v9}, [Ljava/lang/String;

    move-result-object v9

    invoke-static {v1, v5, v3, v9}, Lcom/helpshift/support/external/DoubleMetaphone;->contains(Ljava/lang/String;II[Ljava/lang/String;)Z

    move-result v3

    if-eqz v3, :cond_dd

    goto :goto_44

    :cond_dd
    const/4 v3, 0x1

    :goto_45
    if-lez v7, :cond_e0

    const-string v9, "CHAE"

    .line 221
    filled-new-array {v9}, [Ljava/lang/String;

    move-result-object v9

    const/4 v10, 0x4

    invoke-static {v1, v7, v10, v9}, Lcom/helpshift/support/external/DoubleMetaphone;->contains(Ljava/lang/String;II[Ljava/lang/String;)Z

    move-result v9

    if-eqz v9, :cond_e0

    .line 222
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->primary:Ljava/lang/StringBuilder;

    invoke-virtual {v3}, Ljava/lang/StringBuilder;->length()I

    move-result v3

    iget v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->maxLength:I

    if-ge v3, v4, :cond_de

    .line 223
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->primary:Ljava/lang/StringBuilder;

    const/16 v4, 0x4b

    invoke-virtual {v3, v4}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    .line 225
    :cond_de
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->alternate:Ljava/lang/StringBuilder;

    invoke-virtual {v3}, Ljava/lang/StringBuilder;->length()I

    move-result v3

    iget v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->maxLength:I

    if-ge v3, v4, :cond_df

    .line 226
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->alternate:Ljava/lang/StringBuilder;

    const/16 v4, 0x58

    invoke-virtual {v3, v4}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    :cond_df
    add-int/lit8 v7, v7, 0x2

    goto/16 :goto_43

    :cond_e0
    if-eqz v3, :cond_e3

    .line 232
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->primary:Ljava/lang/StringBuilder;

    invoke-virtual {v3}, Ljava/lang/StringBuilder;->length()I

    move-result v3

    iget v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->maxLength:I

    if-ge v3, v4, :cond_e1

    .line 233
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->primary:Ljava/lang/StringBuilder;

    const/16 v4, 0x4b

    invoke-virtual {v3, v4}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    goto :goto_46

    :cond_e1
    const/16 v4, 0x4b

    .line 235
    :goto_46
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->alternate:Ljava/lang/StringBuilder;

    invoke-virtual {v3}, Ljava/lang/StringBuilder;->length()I

    move-result v3

    iget v9, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->maxLength:I

    if-ge v3, v9, :cond_e2

    .line 236
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->alternate:Ljava/lang/StringBuilder;

    invoke-virtual {v3, v4}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    :cond_e2
    add-int/lit8 v7, v7, 0x2

    goto/16 :goto_43

    :cond_e3
    const-string v3, "VAN "

    const-string v9, "VON "

    .line 240
    filled-new-array {v3, v9}, [Ljava/lang/String;

    move-result-object v3

    const/4 v9, 0x4

    .line 241
    invoke-static {v1, v5, v9, v3}, Lcom/helpshift/support/external/DoubleMetaphone;->contains(Ljava/lang/String;II[Ljava/lang/String;)Z

    move-result v3

    if-nez v3, :cond_eb

    const-string v3, "SCH"

    filled-new-array {v3}, [Ljava/lang/String;

    move-result-object v3

    invoke-static {v1, v5, v4, v3}, Lcom/helpshift/support/external/DoubleMetaphone;->contains(Ljava/lang/String;II[Ljava/lang/String;)Z

    move-result v3

    if-nez v3, :cond_eb

    add-int/lit8 v3, v7, -0x2

    const/4 v4, 0x6

    const-string v9, "ORCHES"

    const-string v10, "ARCHIT"

    const-string v11, "ORCHID"

    filled-new-array {v9, v10, v11}, [Ljava/lang/String;

    move-result-object v9

    .line 242
    invoke-static {v1, v3, v4, v9}, Lcom/helpshift/support/external/DoubleMetaphone;->contains(Ljava/lang/String;II[Ljava/lang/String;)Z

    move-result v3

    if-nez v3, :cond_eb

    add-int/lit8 v3, v7, 0x2

    const-string v4, "T"

    const-string v9, "S"

    filled-new-array {v4, v9}, [Ljava/lang/String;

    move-result-object v4

    .line 243
    invoke-static {v1, v3, v6, v4}, Lcom/helpshift/support/external/DoubleMetaphone;->contains(Ljava/lang/String;II[Ljava/lang/String;)Z

    move-result v4

    if-nez v4, :cond_eb

    add-int/lit8 v4, v7, -0x1

    const-string v9, "A"

    const-string v10, "O"

    const-string v11, "U"

    const-string v13, "E"

    filled-new-array {v9, v10, v11, v13}, [Ljava/lang/String;

    move-result-object v9

    .line 244
    invoke-static {v1, v4, v6, v9}, Lcom/helpshift/support/external/DoubleMetaphone;->contains(Ljava/lang/String;II[Ljava/lang/String;)Z

    move-result v4

    if-nez v4, :cond_e4

    if-nez v7, :cond_e5

    :cond_e4
    sget-object v4, Lcom/helpshift/support/external/DoubleMetaphone;->L_R_N_M_B_H_F_V_W_SPACE:[Ljava/lang/String;

    .line 246
    invoke-static {v1, v3, v6, v4}, Lcom/helpshift/support/external/DoubleMetaphone;->contains(Ljava/lang/String;II[Ljava/lang/String;)Z

    move-result v4

    if-nez v4, :cond_eb

    add-int/lit8 v4, v7, 0x1

    invoke-virtual {v1}, Ljava/lang/String;->length()I

    move-result v9

    sub-int/2addr v9, v6

    if-ne v4, v9, :cond_e5

    goto/16 :goto_49

    :cond_e5
    if-lez v7, :cond_e9

    const-string v4, "MC"

    .line 258
    filled-new-array {v4}, [Ljava/lang/String;

    move-result-object v4

    invoke-static {v1, v5, v14, v4}, Lcom/helpshift/support/external/DoubleMetaphone;->contains(Ljava/lang/String;II[Ljava/lang/String;)Z

    move-result v4

    if-eqz v4, :cond_e7

    .line 259
    iget-object v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->primary:Ljava/lang/StringBuilder;

    invoke-virtual {v4}, Ljava/lang/StringBuilder;->length()I

    move-result v4

    iget v7, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->maxLength:I

    if-ge v4, v7, :cond_e6

    .line 260
    iget-object v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->primary:Ljava/lang/StringBuilder;

    const/16 v7, 0x4b

    invoke-virtual {v4, v7}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    goto :goto_47

    :cond_e6
    const/16 v7, 0x4b

    .line 262
    :goto_47
    iget-object v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->alternate:Ljava/lang/StringBuilder;

    invoke-virtual {v4}, Ljava/lang/StringBuilder;->length()I

    move-result v4

    iget v9, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->maxLength:I

    if-ge v4, v9, :cond_ff

    .line 263
    iget-object v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->alternate:Ljava/lang/StringBuilder;

    invoke-virtual {v4, v7}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    goto/16 :goto_50

    .line 267
    :cond_e7
    iget-object v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->primary:Ljava/lang/StringBuilder;

    invoke-virtual {v4}, Ljava/lang/StringBuilder;->length()I

    move-result v4

    iget v7, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->maxLength:I

    if-ge v4, v7, :cond_e8

    .line 268
    iget-object v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->primary:Ljava/lang/StringBuilder;

    const/16 v7, 0x58

    invoke-virtual {v4, v7}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    .line 270
    :cond_e8
    iget-object v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->alternate:Ljava/lang/StringBuilder;

    invoke-virtual {v4}, Ljava/lang/StringBuilder;->length()I

    move-result v4

    iget v7, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->maxLength:I

    if-ge v4, v7, :cond_ff

    .line 271
    iget-object v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->alternate:Ljava/lang/StringBuilder;

    const/16 v7, 0x4b

    invoke-virtual {v4, v7}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    goto/16 :goto_50

    .line 276
    :cond_e9
    iget-object v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->primary:Ljava/lang/StringBuilder;

    invoke-virtual {v4}, Ljava/lang/StringBuilder;->length()I

    move-result v4

    iget v7, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->maxLength:I

    if-ge v4, v7, :cond_ea

    .line 277
    iget-object v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->primary:Ljava/lang/StringBuilder;

    const/16 v7, 0x58

    invoke-virtual {v4, v7}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    goto :goto_48

    :cond_ea
    const/16 v7, 0x58

    .line 279
    :goto_48
    iget-object v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->alternate:Ljava/lang/StringBuilder;

    invoke-virtual {v4}, Ljava/lang/StringBuilder;->length()I

    move-result v4

    iget v9, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->maxLength:I

    if-ge v4, v9, :cond_ff

    .line 280
    iget-object v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->alternate:Ljava/lang/StringBuilder;

    invoke-virtual {v4, v7}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    goto/16 :goto_50

    .line 248
    :cond_eb
    :goto_49
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->primary:Ljava/lang/StringBuilder;

    invoke-virtual {v3}, Ljava/lang/StringBuilder;->length()I

    move-result v3

    iget v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->maxLength:I

    if-ge v3, v4, :cond_ec

    .line 249
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->primary:Ljava/lang/StringBuilder;

    const/16 v4, 0x4b

    invoke-virtual {v3, v4}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    goto :goto_4a

    :cond_ec
    const/16 v4, 0x4b

    .line 251
    :goto_4a
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->alternate:Ljava/lang/StringBuilder;

    invoke-virtual {v3}, Ljava/lang/StringBuilder;->length()I

    move-result v3

    iget v9, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->maxLength:I

    if-ge v3, v9, :cond_ed

    .line 252
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->alternate:Ljava/lang/StringBuilder;

    invoke-virtual {v3, v4}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    :cond_ed
    add-int/lit8 v7, v7, 0x2

    goto/16 :goto_43

    :cond_ee
    const-string v3, "CZ"

    .line 288
    filled-new-array {v3}, [Ljava/lang/String;

    move-result-object v3

    invoke-static {v1, v7, v14, v3}, Lcom/helpshift/support/external/DoubleMetaphone;->contains(Ljava/lang/String;II[Ljava/lang/String;)Z

    move-result v3

    if-eqz v3, :cond_f1

    add-int/lit8 v3, v7, -0x2

    const-string v9, "WICZ"

    filled-new-array {v9}, [Ljava/lang/String;

    move-result-object v9

    const/4 v13, 0x4

    .line 289
    invoke-static {v1, v3, v13, v9}, Lcom/helpshift/support/external/DoubleMetaphone;->contains(Ljava/lang/String;II[Ljava/lang/String;)Z

    move-result v3

    if-nez v3, :cond_f1

    .line 291
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->primary:Ljava/lang/StringBuilder;

    invoke-virtual {v3}, Ljava/lang/StringBuilder;->length()I

    move-result v3

    iget v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->maxLength:I

    if-ge v3, v4, :cond_ef

    .line 292
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->primary:Ljava/lang/StringBuilder;

    invoke-virtual {v3, v11}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    .line 294
    :cond_ef
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->alternate:Ljava/lang/StringBuilder;

    invoke-virtual {v3}, Ljava/lang/StringBuilder;->length()I

    move-result v3

    iget v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->maxLength:I

    if-ge v3, v4, :cond_f0

    .line 295
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->alternate:Ljava/lang/StringBuilder;

    const/16 v4, 0x58

    invoke-virtual {v3, v4}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    :cond_f0
    add-int/lit8 v7, v7, 0x2

    goto/16 :goto_43

    :cond_f1
    add-int/lit8 v3, v7, 0x1

    const-string v9, "CIA"

    .line 300
    filled-new-array {v9}, [Ljava/lang/String;

    move-result-object v9

    invoke-static {v1, v3, v4, v9}, Lcom/helpshift/support/external/DoubleMetaphone;->contains(Ljava/lang/String;II[Ljava/lang/String;)Z

    move-result v9

    if-eqz v9, :cond_f4

    .line 302
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->primary:Ljava/lang/StringBuilder;

    invoke-virtual {v3}, Ljava/lang/StringBuilder;->length()I

    move-result v3

    iget v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->maxLength:I

    if-ge v3, v4, :cond_f2

    .line 303
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->primary:Ljava/lang/StringBuilder;

    const/16 v4, 0x58

    invoke-virtual {v3, v4}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    goto :goto_4b

    :cond_f2
    const/16 v4, 0x58

    .line 305
    :goto_4b
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->alternate:Ljava/lang/StringBuilder;

    invoke-virtual {v3}, Ljava/lang/StringBuilder;->length()I

    move-result v3

    iget v9, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->maxLength:I

    if-ge v3, v9, :cond_f3

    .line 306
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->alternate:Ljava/lang/StringBuilder;

    invoke-virtual {v3, v4}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    :cond_f3
    add-int/lit8 v7, v7, 0x3

    goto/16 :goto_43

    :cond_f4
    const-string v9, "CC"

    .line 311
    filled-new-array {v9}, [Ljava/lang/String;

    move-result-object v9

    invoke-static {v1, v7, v14, v9}, Lcom/helpshift/support/external/DoubleMetaphone;->contains(Ljava/lang/String;II[Ljava/lang/String;)Z

    move-result v9

    if-eqz v9, :cond_100

    if-ne v7, v6, :cond_f5

    .line 312
    invoke-virtual {v0, v1, v5}, Lcom/helpshift/support/external/DoubleMetaphone;->charAt(Ljava/lang/String;I)C

    move-result v9

    const/16 v13, 0x4d

    if-eq v9, v13, :cond_100

    :cond_f5
    add-int/lit8 v3, v7, 0x2

    const-string v4, "I"

    const-string v9, "E"

    const-string v11, "H"

    .line 315
    filled-new-array {v4, v9, v11}, [Ljava/lang/String;

    move-result-object v4

    invoke-static {v1, v3, v6, v4}, Lcom/helpshift/support/external/DoubleMetaphone;->contains(Ljava/lang/String;II[Ljava/lang/String;)Z

    move-result v4

    if-eqz v4, :cond_fd

    const-string v4, "HU"

    filled-new-array {v4}, [Ljava/lang/String;

    move-result-object v4

    .line 316
    invoke-static {v1, v3, v14, v4}, Lcom/helpshift/support/external/DoubleMetaphone;->contains(Ljava/lang/String;II[Ljava/lang/String;)Z

    move-result v4

    if-nez v4, :cond_fd

    if-ne v7, v6, :cond_f6

    add-int/lit8 v3, v7, -0x1

    .line 318
    invoke-virtual {v0, v1, v3}, Lcom/helpshift/support/external/DoubleMetaphone;->charAt(Ljava/lang/String;I)C

    move-result v3

    if-eq v3, v10, :cond_f7

    :cond_f6
    add-int/lit8 v3, v7, -0x1

    const/4 v4, 0x5

    const-string v9, "UCCEE"

    const-string v10, "UCCES"

    filled-new-array {v9, v10}, [Ljava/lang/String;

    move-result-object v9

    .line 319
    invoke-static {v1, v3, v4, v9}, Lcom/helpshift/support/external/DoubleMetaphone;->contains(Ljava/lang/String;II[Ljava/lang/String;)Z

    move-result v3

    if-eqz v3, :cond_fa

    .line 321
    :cond_f7
    iget v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->maxLength:I

    iget-object v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->primary:Ljava/lang/StringBuilder;

    invoke-virtual {v4}, Ljava/lang/StringBuilder;->length()I

    move-result v4

    sub-int/2addr v3, v4

    const-string v4, "KS"

    .line 322
    invoke-virtual {v4}, Ljava/lang/String;->length()I

    move-result v4

    if-gt v4, v3, :cond_f8

    .line 323
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->primary:Ljava/lang/StringBuilder;

    const-string v4, "KS"

    invoke-virtual {v3, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    goto :goto_4c

    .line 326
    :cond_f8
    iget-object v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->primary:Ljava/lang/StringBuilder;

    const-string v9, "KS"

    invoke-virtual {v9, v5, v3}, Ljava/lang/String;->substring(II)Ljava/lang/String;

    move-result-object v3

    invoke-virtual {v4, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    .line 328
    :goto_4c
    iget v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->maxLength:I

    iget-object v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->alternate:Ljava/lang/StringBuilder;

    invoke-virtual {v4}, Ljava/lang/StringBuilder;->length()I

    move-result v4

    sub-int/2addr v3, v4

    const-string v4, "KS"

    .line 329
    invoke-virtual {v4}, Ljava/lang/String;->length()I

    move-result v4

    if-gt v4, v3, :cond_f9

    .line 330
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->alternate:Ljava/lang/StringBuilder;

    const-string v4, "KS"

    invoke-virtual {v3, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    goto :goto_4e

    .line 333
    :cond_f9
    iget-object v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->alternate:Ljava/lang/StringBuilder;

    const-string v9, "KS"

    invoke-virtual {v9, v5, v3}, Ljava/lang/String;->substring(II)Ljava/lang/String;

    move-result-object v3

    invoke-virtual {v4, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    goto :goto_4e

    .line 338
    :cond_fa
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->primary:Ljava/lang/StringBuilder;

    invoke-virtual {v3}, Ljava/lang/StringBuilder;->length()I

    move-result v3

    iget v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->maxLength:I

    if-ge v3, v4, :cond_fb

    .line 339
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->primary:Ljava/lang/StringBuilder;

    const/16 v4, 0x58

    invoke-virtual {v3, v4}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    goto :goto_4d

    :cond_fb
    const/16 v4, 0x58

    .line 341
    :goto_4d
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->alternate:Ljava/lang/StringBuilder;

    invoke-virtual {v3}, Ljava/lang/StringBuilder;->length()I

    move-result v3

    iget v9, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->maxLength:I

    if-ge v3, v9, :cond_fc

    .line 342
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->alternate:Ljava/lang/StringBuilder;

    invoke-virtual {v3, v4}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    :cond_fc
    :goto_4e
    add-int/lit8 v3, v7, 0x3

    goto :goto_50

    .line 348
    :cond_fd
    iget-object v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->primary:Ljava/lang/StringBuilder;

    invoke-virtual {v4}, Ljava/lang/StringBuilder;->length()I

    move-result v4

    iget v7, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->maxLength:I

    if-ge v4, v7, :cond_fe

    .line 349
    iget-object v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->primary:Ljava/lang/StringBuilder;

    const/16 v7, 0x4b

    invoke-virtual {v4, v7}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    goto :goto_4f

    :cond_fe
    const/16 v7, 0x4b

    .line 351
    :goto_4f
    iget-object v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->alternate:Ljava/lang/StringBuilder;

    invoke-virtual {v4}, Ljava/lang/StringBuilder;->length()I

    move-result v4

    iget v9, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->maxLength:I

    if-ge v4, v9, :cond_ff

    .line 352
    iget-object v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->alternate:Ljava/lang/StringBuilder;

    invoke-virtual {v4, v7}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    :cond_ff
    :goto_50
    move v7, v3

    goto/16 :goto_43

    :cond_100
    const-string v9, "CK"

    const-string v10, "CG"

    const-string v13, "CQ"

    .line 359
    filled-new-array {v9, v10, v13}, [Ljava/lang/String;

    move-result-object v9

    invoke-static {v1, v7, v14, v9}, Lcom/helpshift/support/external/DoubleMetaphone;->contains(Ljava/lang/String;II[Ljava/lang/String;)Z

    move-result v9

    if-eqz v9, :cond_103

    .line 360
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->primary:Ljava/lang/StringBuilder;

    invoke-virtual {v3}, Ljava/lang/StringBuilder;->length()I

    move-result v3

    iget v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->maxLength:I

    if-ge v3, v4, :cond_101

    .line 361
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->primary:Ljava/lang/StringBuilder;

    const/16 v4, 0x4b

    invoke-virtual {v3, v4}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    goto :goto_51

    :cond_101
    const/16 v4, 0x4b

    .line 363
    :goto_51
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->alternate:Ljava/lang/StringBuilder;

    invoke-virtual {v3}, Ljava/lang/StringBuilder;->length()I

    move-result v3

    iget v9, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->maxLength:I

    if-ge v3, v9, :cond_102

    .line 364
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->alternate:Ljava/lang/StringBuilder;

    invoke-virtual {v3, v4}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    :cond_102
    add-int/lit8 v7, v7, 0x2

    goto/16 :goto_43

    :cond_103
    const-string v9, "CI"

    const-string v10, "CE"

    const-string v13, "CY"

    .line 369
    filled-new-array {v9, v10, v13}, [Ljava/lang/String;

    move-result-object v9

    invoke-static {v1, v7, v14, v9}, Lcom/helpshift/support/external/DoubleMetaphone;->contains(Ljava/lang/String;II[Ljava/lang/String;)Z

    move-result v9

    if-eqz v9, :cond_108

    const-string v3, "CIO"

    const-string v9, "CIE"

    const-string v10, "CIA"

    .line 371
    filled-new-array {v3, v9, v10}, [Ljava/lang/String;

    move-result-object v3

    invoke-static {v1, v7, v4, v3}, Lcom/helpshift/support/external/DoubleMetaphone;->contains(Ljava/lang/String;II[Ljava/lang/String;)Z

    move-result v3

    if-eqz v3, :cond_105

    .line 372
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->primary:Ljava/lang/StringBuilder;

    invoke-virtual {v3}, Ljava/lang/StringBuilder;->length()I

    move-result v3

    iget v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->maxLength:I

    if-ge v3, v4, :cond_104

    .line 373
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->primary:Ljava/lang/StringBuilder;

    invoke-virtual {v3, v11}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    .line 375
    :cond_104
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->alternate:Ljava/lang/StringBuilder;

    invoke-virtual {v3}, Ljava/lang/StringBuilder;->length()I

    move-result v3

    iget v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->maxLength:I

    if-ge v3, v4, :cond_107

    .line 376
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->alternate:Ljava/lang/StringBuilder;

    const/16 v4, 0x58

    invoke-virtual {v3, v4}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    goto :goto_52

    .line 380
    :cond_105
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->primary:Ljava/lang/StringBuilder;

    invoke-virtual {v3}, Ljava/lang/StringBuilder;->length()I

    move-result v3

    iget v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->maxLength:I

    if-ge v3, v4, :cond_106

    .line 381
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->primary:Ljava/lang/StringBuilder;

    invoke-virtual {v3, v11}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    .line 383
    :cond_106
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->alternate:Ljava/lang/StringBuilder;

    invoke-virtual {v3}, Ljava/lang/StringBuilder;->length()I

    move-result v3

    iget v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->maxLength:I

    if-ge v3, v4, :cond_107

    .line 384
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->alternate:Ljava/lang/StringBuilder;

    invoke-virtual {v3, v11}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    :cond_107
    :goto_52
    add-int/lit8 v7, v7, 0x2

    goto/16 :goto_43

    .line 391
    :cond_108
    iget-object v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->primary:Ljava/lang/StringBuilder;

    invoke-virtual {v4}, Ljava/lang/StringBuilder;->length()I

    move-result v4

    iget v9, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->maxLength:I

    if-ge v4, v9, :cond_109

    .line 392
    iget-object v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->primary:Ljava/lang/StringBuilder;

    const/16 v9, 0x4b

    invoke-virtual {v4, v9}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    goto :goto_53

    :cond_109
    const/16 v9, 0x4b

    .line 394
    :goto_53
    iget-object v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->alternate:Ljava/lang/StringBuilder;

    invoke-virtual {v4}, Ljava/lang/StringBuilder;->length()I

    move-result v4

    iget v10, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->maxLength:I

    if-ge v4, v10, :cond_10a

    .line 395
    iget-object v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->alternate:Ljava/lang/StringBuilder;

    invoke-virtual {v4, v9}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    :cond_10a
    const-string v4, " C"

    const-string v10, " Q"

    const-string v11, " G"

    .line 397
    filled-new-array {v4, v10, v11}, [Ljava/lang/String;

    move-result-object v4

    invoke-static {v1, v3, v14, v4}, Lcom/helpshift/support/external/DoubleMetaphone;->contains(Ljava/lang/String;II[Ljava/lang/String;)Z

    move-result v4

    if-eqz v4, :cond_10b

    add-int/lit8 v3, v7, 0x3

    goto/16 :goto_17

    :cond_10b
    const-string v4, "C"

    const-string v10, "K"

    const-string v11, "Q"

    .line 401
    filled-new-array {v4, v10, v11}, [Ljava/lang/String;

    move-result-object v4

    invoke-static {v1, v3, v6, v4}, Lcom/helpshift/support/external/DoubleMetaphone;->contains(Ljava/lang/String;II[Ljava/lang/String;)Z

    move-result v4

    if-eqz v4, :cond_47

    const-string v4, "CE"

    const-string v10, "CI"

    filled-new-array {v4, v10}, [Ljava/lang/String;

    move-result-object v4

    .line 402
    invoke-static {v1, v3, v14, v4}, Lcom/helpshift/support/external/DoubleMetaphone;->contains(Ljava/lang/String;II[Ljava/lang/String;)Z

    move-result v4

    if-nez v4, :cond_47

    add-int/lit8 v3, v7, 0x2

    goto/16 :goto_17

    :pswitch_13
    const/16 v9, 0x4b

    const/4 v12, -0x1

    .line 145
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->primary:Ljava/lang/StringBuilder;

    invoke-virtual {v3}, Ljava/lang/StringBuilder;->length()I

    move-result v3

    iget v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->maxLength:I

    if-ge v3, v4, :cond_10c

    .line 146
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->primary:Ljava/lang/StringBuilder;

    const/16 v4, 0x50

    invoke-virtual {v3, v4}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    .line 148
    :cond_10c
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->alternate:Ljava/lang/StringBuilder;

    invoke-virtual {v3}, Ljava/lang/StringBuilder;->length()I

    move-result v3

    iget v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->maxLength:I

    if-ge v3, v4, :cond_10d

    .line 149
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->alternate:Ljava/lang/StringBuilder;

    const/16 v4, 0x50

    invoke-virtual {v3, v4}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    :cond_10d
    add-int/lit8 v3, v7, 0x1

    .line 151
    invoke-virtual {v0, v1, v3}, Lcom/helpshift/support/external/DoubleMetaphone;->charAt(Ljava/lang/String;I)C

    move-result v4

    const/16 v10, 0x42

    if-ne v4, v10, :cond_47

    add-int/lit8 v7, v7, 0x2

    goto :goto_54

    :pswitch_14
    const/16 v9, 0x4b

    const/4 v12, -0x1

    if-nez v7, :cond_10f

    .line 135
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->primary:Ljava/lang/StringBuilder;

    invoke-virtual {v3}, Ljava/lang/StringBuilder;->length()I

    move-result v3

    iget v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->maxLength:I

    if-ge v3, v4, :cond_10e

    .line 136
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->primary:Ljava/lang/StringBuilder;

    invoke-virtual {v3, v10}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    .line 138
    :cond_10e
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->alternate:Ljava/lang/StringBuilder;

    invoke-virtual {v3}, Ljava/lang/StringBuilder;->length()I

    move-result v3

    iget v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->maxLength:I

    if-ge v3, v4, :cond_10f

    .line 139
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->alternate:Ljava/lang/StringBuilder;

    invoke-virtual {v3, v10}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    :cond_10f
    add-int/lit8 v7, v7, 0x1

    :cond_110
    :goto_54
    const/4 v3, -0x1

    goto/16 :goto_5

    :cond_111
    const/16 v9, 0x4b

    const/4 v12, -0x1

    .line 851
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->primary:Ljava/lang/StringBuilder;

    invoke-virtual {v3}, Ljava/lang/StringBuilder;->length()I

    move-result v3

    iget v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->maxLength:I

    if-ge v3, v4, :cond_112

    .line 852
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->primary:Ljava/lang/StringBuilder;

    const/16 v4, 0x4e

    invoke-virtual {v3, v4}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    .line 854
    :cond_112
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->alternate:Ljava/lang/StringBuilder;

    invoke-virtual {v3}, Ljava/lang/StringBuilder;->length()I

    move-result v3

    iget v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->maxLength:I

    if-ge v3, v4, :cond_113

    .line 855
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->alternate:Ljava/lang/StringBuilder;

    const/16 v4, 0x4e

    invoke-virtual {v3, v4}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    :cond_113
    add-int/lit8 v7, v7, 0x1

    goto :goto_54

    :cond_114
    const/16 v9, 0x4b

    const/4 v12, -0x1

    .line 155
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->primary:Ljava/lang/StringBuilder;

    invoke-virtual {v3}, Ljava/lang/StringBuilder;->length()I

    move-result v3

    iget v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->maxLength:I

    if-ge v3, v4, :cond_115

    .line 156
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->primary:Ljava/lang/StringBuilder;

    invoke-virtual {v3, v11}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    .line 158
    :cond_115
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->alternate:Ljava/lang/StringBuilder;

    invoke-virtual {v3}, Ljava/lang/StringBuilder;->length()I

    move-result v3

    iget v4, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->maxLength:I

    if-ge v3, v4, :cond_116

    .line 159
    iget-object v3, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->alternate:Ljava/lang/StringBuilder;

    invoke-virtual {v3, v11}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    :cond_116
    add-int/lit8 v7, v7, 0x1

    goto :goto_54

    :cond_117
    if-eqz p2, :cond_118

    .line 1301
    iget-object v1, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->alternate:Ljava/lang/StringBuilder;

    :goto_55
    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    goto :goto_56

    :cond_118
    iget-object v1, v8, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->primary:Ljava/lang/StringBuilder;

    goto :goto_55

    :goto_56
    return-object v1

    nop

    :pswitch_data_0
    .packed-switch 0x41
        :pswitch_14
        :pswitch_13
        :pswitch_12
        :pswitch_11
        :pswitch_14
        :pswitch_10
        :pswitch_f
        :pswitch_e
        :pswitch_14
        :pswitch_d
        :pswitch_c
        :pswitch_b
        :pswitch_a
        :pswitch_9
        :pswitch_14
        :pswitch_8
        :pswitch_7
        :pswitch_6
        :pswitch_5
        :pswitch_4
        :pswitch_14
        :pswitch_3
        :pswitch_2
        :pswitch_1
        :pswitch_14
        :pswitch_0
    .end packed-switch
.end method
