.class final Lcom/facebook/appevents/ml/Operator;
.super Ljava/lang/Object;
.source "Operator.java"


# direct methods
.method constructor <init>()V
    .locals 0

    .line 26
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method

.method static addmv(Lcom/facebook/appevents/ml/MTensor;Lcom/facebook/appevents/ml/MTensor;)V
    .locals 10

    const-class v0, Lcom/facebook/appevents/ml/Operator;

    invoke-static {v0}, Lcom/facebook/internal/instrument/crashshield/CrashShieldHandler;->isObjectCrashing(Ljava/lang/Object;)Z

    move-result v0

    if-eqz v0, :cond_0

    return-void

    :cond_0
    const/4 v0, 0x0

    .line 29
    :try_start_0
    invoke-virtual {p0, v0}, Lcom/facebook/appevents/ml/MTensor;->getShape(I)I

    move-result v1

    const/4 v2, 0x1

    .line 30
    invoke-virtual {p0, v2}, Lcom/facebook/appevents/ml/MTensor;->getShape(I)I

    move-result v2

    const/4 v3, 0x2

    .line 31
    invoke-virtual {p0, v3}, Lcom/facebook/appevents/ml/MTensor;->getShape(I)I

    move-result v3

    .line 32
    invoke-virtual {p0}, Lcom/facebook/appevents/ml/MTensor;->getData()[F

    move-result-object p0

    .line 33
    invoke-virtual {p1}, Lcom/facebook/appevents/ml/MTensor;->getData()[F

    move-result-object p1

    const/4 v4, 0x0

    :goto_0
    if-ge v4, v1, :cond_3

    const/4 v5, 0x0

    :goto_1
    if-ge v5, v2, :cond_2

    const/4 v6, 0x0

    :goto_2
    if-ge v6, v3, :cond_1

    mul-int v7, v4, v2

    mul-int v7, v7, v3

    mul-int v8, v5, v3

    add-int/2addr v7, v8

    add-int/2addr v7, v6

    .line 38
    aget v8, p0, v7

    aget v9, p1, v6

    add-float/2addr v8, v9

    aput v8, p0, v7
    :try_end_0
    .catch Ljava/lang/Throwable; {:try_start_0 .. :try_end_0} :catch_0

    add-int/lit8 v6, v6, 0x1

    goto :goto_2

    :cond_1
    add-int/lit8 v5, v5, 0x1

    goto :goto_1

    :cond_2
    add-int/lit8 v4, v4, 0x1

    goto :goto_0

    :cond_3
    return-void

    :catch_0
    move-exception p0

    .line 42
    const-class p1, Lcom/facebook/appevents/ml/Operator;

    invoke-static {p0, p1}, Lcom/facebook/internal/instrument/crashshield/CrashShieldHandler;->handleThrowable(Ljava/lang/Throwable;Ljava/lang/Object;)V

    return-void
.end method

.method static concatenate([Lcom/facebook/appevents/ml/MTensor;)Lcom/facebook/appevents/ml/MTensor;
    .locals 13

    const-class v0, Lcom/facebook/appevents/ml/Operator;

    invoke-static {v0}, Lcom/facebook/internal/instrument/crashshield/CrashShieldHandler;->isObjectCrashing(Ljava/lang/Object;)Z

    move-result v0

    const/4 v1, 0x0

    if-eqz v0, :cond_0

    return-object v1

    :cond_0
    const/4 v0, 0x0

    .line 90
    :try_start_0
    aget-object v2, p0, v0

    invoke-virtual {v2, v0}, Lcom/facebook/appevents/ml/MTensor;->getShape(I)I

    move-result v2

    const/4 v3, 0x0

    const/4 v4, 0x0

    .line 92
    :goto_0
    array-length v5, p0

    const/4 v6, 0x1

    if-ge v3, v5, :cond_1

    .line 93
    aget-object v5, p0, v3

    invoke-virtual {v5, v6}, Lcom/facebook/appevents/ml/MTensor;->getShape(I)I

    move-result v5

    add-int/2addr v4, v5

    add-int/lit8 v3, v3, 0x1

    goto :goto_0

    .line 95
    :cond_1
    new-instance v3, Lcom/facebook/appevents/ml/MTensor;

    const/4 v5, 0x2

    new-array v5, v5, [I

    aput v2, v5, v0

    aput v4, v5, v6

    invoke-direct {v3, v5}, Lcom/facebook/appevents/ml/MTensor;-><init>([I)V

    .line 96
    invoke-virtual {v3}, Lcom/facebook/appevents/ml/MTensor;->getData()[F

    move-result-object v5

    const/4 v7, 0x0

    :goto_1
    if-ge v7, v2, :cond_3

    mul-int v8, v7, v4

    move v9, v8

    const/4 v8, 0x0

    .line 100
    :goto_2
    array-length v10, p0

    if-ge v8, v10, :cond_2

    .line 101
    aget-object v10, p0, v8

    invoke-virtual {v10}, Lcom/facebook/appevents/ml/MTensor;->getData()[F

    move-result-object v10

    .line 102
    aget-object v11, p0, v8

    invoke-virtual {v11, v6}, Lcom/facebook/appevents/ml/MTensor;->getShape(I)I

    move-result v11

    mul-int v12, v7, v11

    .line 103
    invoke-static {v10, v12, v5, v9, v11}, Ljava/lang/System;->arraycopy(Ljava/lang/Object;ILjava/lang/Object;II)V
    :try_end_0
    .catch Ljava/lang/Throwable; {:try_start_0 .. :try_end_0} :catch_0

    add-int/2addr v9, v11

    add-int/lit8 v8, v8, 0x1

    goto :goto_2

    :cond_2
    add-int/lit8 v7, v7, 0x1

    goto :goto_1

    :cond_3
    return-object v3

    :catch_0
    move-exception p0

    .line 107
    const-class v0, Lcom/facebook/appevents/ml/Operator;

    invoke-static {p0, v0}, Lcom/facebook/internal/instrument/crashshield/CrashShieldHandler;->handleThrowable(Ljava/lang/Throwable;Ljava/lang/Object;)V

    return-object v1
.end method

.method static conv1D(Lcom/facebook/appevents/ml/MTensor;Lcom/facebook/appevents/ml/MTensor;)Lcom/facebook/appevents/ml/MTensor;
    .locals 19

    move-object/from16 v0, p0

    move-object/from16 v1, p1

    const-class v2, Lcom/facebook/appevents/ml/Operator;

    invoke-static {v2}, Lcom/facebook/internal/instrument/crashshield/CrashShieldHandler;->isObjectCrashing(Ljava/lang/Object;)Z

    move-result v2

    const/4 v3, 0x0

    if-eqz v2, :cond_0

    return-object v3

    :cond_0
    const/4 v2, 0x0

    .line 205
    :try_start_0
    invoke-virtual {v0, v2}, Lcom/facebook/appevents/ml/MTensor;->getShape(I)I

    move-result v4

    const/4 v5, 0x1

    .line 206
    invoke-virtual {v0, v5}, Lcom/facebook/appevents/ml/MTensor;->getShape(I)I

    move-result v6

    const/4 v7, 0x2

    .line 207
    invoke-virtual {v0, v7}, Lcom/facebook/appevents/ml/MTensor;->getShape(I)I

    move-result v8

    .line 208
    invoke-virtual {v1, v2}, Lcom/facebook/appevents/ml/MTensor;->getShape(I)I

    move-result v9

    sub-int v10, v6, v9

    add-int/2addr v10, v5

    .line 210
    invoke-virtual {v1, v7}, Lcom/facebook/appevents/ml/MTensor;->getShape(I)I

    move-result v11

    .line 211
    new-instance v12, Lcom/facebook/appevents/ml/MTensor;

    const/4 v13, 0x3

    new-array v13, v13, [I

    aput v4, v13, v2

    aput v10, v13, v5

    aput v11, v13, v7

    invoke-direct {v12, v13}, Lcom/facebook/appevents/ml/MTensor;-><init>([I)V

    .line 212
    invoke-virtual/range {p0 .. p0}, Lcom/facebook/appevents/ml/MTensor;->getData()[F

    move-result-object v0

    .line 213
    invoke-virtual {v12}, Lcom/facebook/appevents/ml/MTensor;->getData()[F

    move-result-object v5

    .line 214
    invoke-virtual/range {p1 .. p1}, Lcom/facebook/appevents/ml/MTensor;->getData()[F

    move-result-object v1

    const/4 v7, 0x0

    :goto_0
    if-ge v7, v4, :cond_5

    const/4 v13, 0x0

    :goto_1
    if-ge v13, v11, :cond_4

    const/4 v14, 0x0

    :goto_2
    if-ge v14, v10, :cond_3

    const/4 v15, 0x0

    const/4 v15, 0x0

    const/16 v16, 0x0

    :goto_3
    if-ge v15, v9, :cond_2

    :goto_4
    if-ge v2, v8, :cond_1

    mul-int v17, v6, v8

    mul-int v17, v17, v7

    add-int v18, v15, v14

    mul-int v18, v18, v8

    add-int v17, v17, v18

    add-int v17, v17, v2

    .line 222
    aget v17, v0, v17

    mul-int v18, v15, v8

    add-int v18, v18, v2

    mul-int v18, v18, v11

    add-int v18, v18, v13

    aget v18, v1, v18

    mul-float v17, v17, v18

    add-float v16, v16, v17

    add-int/lit8 v2, v2, 0x1

    goto :goto_4

    :cond_1
    add-int/lit8 v15, v15, 0x1

    const/4 v2, 0x0

    goto :goto_3

    :cond_2
    mul-int v2, v10, v11

    mul-int v2, v2, v7

    mul-int v15, v14, v11

    add-int/2addr v2, v15

    add-int/2addr v2, v13

    .line 226
    aput v16, v5, v2
    :try_end_0
    .catch Ljava/lang/Throwable; {:try_start_0 .. :try_end_0} :catch_0

    add-int/lit8 v14, v14, 0x1

    const/4 v2, 0x0

    goto :goto_2

    :cond_3
    add-int/lit8 v13, v13, 0x1

    const/4 v2, 0x0

    goto :goto_1

    :cond_4
    add-int/lit8 v7, v7, 0x1

    const/4 v2, 0x0

    goto :goto_0

    :cond_5
    return-object v12

    :catch_0
    move-exception v0

    .line 230
    const-class v1, Lcom/facebook/appevents/ml/Operator;

    invoke-static {v0, v1}, Lcom/facebook/internal/instrument/crashshield/CrashShieldHandler;->handleThrowable(Ljava/lang/Throwable;Ljava/lang/Object;)V

    return-object v3
.end method

.method static dense(Lcom/facebook/appevents/ml/MTensor;Lcom/facebook/appevents/ml/MTensor;Lcom/facebook/appevents/ml/MTensor;)Lcom/facebook/appevents/ml/MTensor;
    .locals 9

    const-class v0, Lcom/facebook/appevents/ml/Operator;

    invoke-static {v0}, Lcom/facebook/internal/instrument/crashshield/CrashShieldHandler;->isObjectCrashing(Ljava/lang/Object;)Z

    move-result v0

    const/4 v1, 0x0

    if-eqz v0, :cond_0

    return-object v1

    :cond_0
    const/4 v0, 0x0

    .line 141
    :try_start_0
    invoke-virtual {p0, v0}, Lcom/facebook/appevents/ml/MTensor;->getShape(I)I

    move-result v2

    .line 142
    invoke-virtual {p2, v0}, Lcom/facebook/appevents/ml/MTensor;->getShape(I)I

    move-result v3

    .line 143
    invoke-static {p0, p1}, Lcom/facebook/appevents/ml/Operator;->mul(Lcom/facebook/appevents/ml/MTensor;Lcom/facebook/appevents/ml/MTensor;)Lcom/facebook/appevents/ml/MTensor;

    move-result-object p0

    .line 144
    invoke-virtual {p2}, Lcom/facebook/appevents/ml/MTensor;->getData()[F

    move-result-object p1

    .line 145
    invoke-virtual {p0}, Lcom/facebook/appevents/ml/MTensor;->getData()[F

    move-result-object p2

    const/4 v4, 0x0

    :goto_0
    if-ge v4, v2, :cond_2

    const/4 v5, 0x0

    :goto_1
    if-ge v5, v3, :cond_1

    mul-int v6, v4, v3

    add-int/2addr v6, v5

    .line 149
    aget v7, p2, v6

    aget v8, p1, v5

    add-float/2addr v7, v8

    aput v7, p2, v6
    :try_end_0
    .catch Ljava/lang/Throwable; {:try_start_0 .. :try_end_0} :catch_0

    add-int/lit8 v5, v5, 0x1

    goto :goto_1

    :cond_1
    add-int/lit8 v4, v4, 0x1

    goto :goto_0

    :cond_2
    return-object p0

    :catch_0
    move-exception p0

    .line 152
    const-class p1, Lcom/facebook/appevents/ml/Operator;

    invoke-static {p0, p1}, Lcom/facebook/internal/instrument/crashshield/CrashShieldHandler;->handleThrowable(Ljava/lang/Throwable;Ljava/lang/Object;)V

    return-object v1
.end method

.method static embedding([Ljava/lang/String;ILcom/facebook/appevents/ml/MTensor;)Lcom/facebook/appevents/ml/MTensor;
    .locals 12

    const-class v0, Lcom/facebook/appevents/ml/Operator;

    invoke-static {v0}, Lcom/facebook/internal/instrument/crashshield/CrashShieldHandler;->isObjectCrashing(Ljava/lang/Object;)Z

    move-result v0

    const/4 v1, 0x0

    if-eqz v0, :cond_0

    return-object v1

    .line 156
    :cond_0
    :try_start_0
    array-length v0, p0

    const/4 v2, 0x1

    .line 157
    invoke-virtual {p2, v2}, Lcom/facebook/appevents/ml/MTensor;->getShape(I)I

    move-result v3

    .line 158
    new-instance v4, Lcom/facebook/appevents/ml/MTensor;

    const/4 v5, 0x3

    new-array v5, v5, [I

    const/4 v6, 0x0

    aput v0, v5, v6

    aput p1, v5, v2

    const/4 v2, 0x2

    aput v3, v5, v2

    invoke-direct {v4, v5}, Lcom/facebook/appevents/ml/MTensor;-><init>([I)V

    .line 159
    invoke-virtual {v4}, Lcom/facebook/appevents/ml/MTensor;->getData()[F

    move-result-object v2

    .line 160
    invoke-virtual {p2}, Lcom/facebook/appevents/ml/MTensor;->getData()[F

    move-result-object p2

    const/4 v5, 0x0

    :goto_0
    if-ge v5, v0, :cond_2

    .line 163
    aget-object v7, p0, v5

    invoke-static {v7, p1}, Lcom/facebook/appevents/ml/Utils;->vectorize(Ljava/lang/String;I)[I

    move-result-object v7

    const/4 v8, 0x0

    :goto_1
    if-ge v8, p1, :cond_1

    .line 165
    aget v9, v7, v8

    mul-int v9, v9, v3

    mul-int v10, v3, p1

    mul-int v10, v10, v5

    mul-int v11, v3, v8

    add-int/2addr v10, v11

    invoke-static {p2, v9, v2, v10, v3}, Ljava/lang/System;->arraycopy(Ljava/lang/Object;ILjava/lang/Object;II)V
    :try_end_0
    .catch Ljava/lang/Throwable; {:try_start_0 .. :try_end_0} :catch_0

    add-int/lit8 v8, v8, 0x1

    goto :goto_1

    :cond_1
    add-int/lit8 v5, v5, 0x1

    goto :goto_0

    :cond_2
    return-object v4

    :catch_0
    move-exception p0

    .line 168
    const-class p1, Lcom/facebook/appevents/ml/Operator;

    invoke-static {p0, p1}, Lcom/facebook/internal/instrument/crashshield/CrashShieldHandler;->handleThrowable(Ljava/lang/Throwable;Ljava/lang/Object;)V

    return-object v1
.end method

.method static flatten(Lcom/facebook/appevents/ml/MTensor;I)V
    .locals 4

    const-class v0, Lcom/facebook/appevents/ml/Operator;

    invoke-static {v0}, Lcom/facebook/internal/instrument/crashshield/CrashShieldHandler;->isObjectCrashing(Ljava/lang/Object;)Z

    move-result v0

    if-eqz v0, :cond_0

    return-void

    .line 74
    :cond_0
    :try_start_0
    invoke-virtual {p0}, Lcom/facebook/appevents/ml/MTensor;->getShapeSize()I

    move-result v0

    if-lt p1, v0, :cond_1

    return-void

    :cond_1
    const/4 v0, 0x1

    move v0, p1

    const/4 v1, 0x1

    .line 78
    :goto_0
    invoke-virtual {p0}, Lcom/facebook/appevents/ml/MTensor;->getShapeSize()I

    move-result v2

    if-ge v0, v2, :cond_2

    .line 79
    invoke-virtual {p0, v0}, Lcom/facebook/appevents/ml/MTensor;->getShape(I)I

    move-result v2

    mul-int v1, v1, v2

    add-int/lit8 v0, v0, 0x1

    goto :goto_0

    :cond_2
    add-int/lit8 v0, p1, 0x1

    .line 81
    new-array v0, v0, [I

    const/4 v2, 0x0

    :goto_1
    if-ge v2, p1, :cond_3

    .line 83
    invoke-virtual {p0, v2}, Lcom/facebook/appevents/ml/MTensor;->getShape(I)I

    move-result v3

    aput v3, v0, v2

    add-int/lit8 v2, v2, 0x1

    goto :goto_1

    .line 85
    :cond_3
    aput v1, v0, p1

    .line 86
    invoke-virtual {p0, v0}, Lcom/facebook/appevents/ml/MTensor;->reshape([I)V
    :try_end_0
    .catch Ljava/lang/Throwable; {:try_start_0 .. :try_end_0} :catch_0

    return-void

    :catch_0
    move-exception p0

    .line 87
    const-class p1, Lcom/facebook/appevents/ml/Operator;

    invoke-static {p0, p1}, Lcom/facebook/internal/instrument/crashshield/CrashShieldHandler;->handleThrowable(Ljava/lang/Throwable;Ljava/lang/Object;)V

    return-void
.end method

.method static maxPool1D(Lcom/facebook/appevents/ml/MTensor;I)Lcom/facebook/appevents/ml/MTensor;
    .locals 17

    move-object/from16 v0, p0

    move/from16 v1, p1

    const-class v2, Lcom/facebook/appevents/ml/Operator;

    invoke-static {v2}, Lcom/facebook/internal/instrument/crashshield/CrashShieldHandler;->isObjectCrashing(Ljava/lang/Object;)Z

    move-result v2

    const/4 v3, 0x0

    if-eqz v2, :cond_0

    return-object v3

    :cond_0
    const/4 v2, 0x0

    .line 234
    :try_start_0
    invoke-virtual {v0, v2}, Lcom/facebook/appevents/ml/MTensor;->getShape(I)I

    move-result v4

    const/4 v5, 0x1

    .line 235
    invoke-virtual {v0, v5}, Lcom/facebook/appevents/ml/MTensor;->getShape(I)I

    move-result v6

    const/4 v7, 0x2

    .line 236
    invoke-virtual {v0, v7}, Lcom/facebook/appevents/ml/MTensor;->getShape(I)I

    move-result v8

    sub-int v9, v6, v1

    add-int/2addr v9, v5

    .line 238
    new-instance v10, Lcom/facebook/appevents/ml/MTensor;

    const/4 v11, 0x3

    new-array v11, v11, [I

    aput v4, v11, v2

    aput v9, v11, v5

    aput v8, v11, v7

    invoke-direct {v10, v11}, Lcom/facebook/appevents/ml/MTensor;-><init>([I)V

    .line 239
    invoke-virtual/range {p0 .. p0}, Lcom/facebook/appevents/ml/MTensor;->getData()[F

    move-result-object v0

    .line 240
    invoke-virtual {v10}, Lcom/facebook/appevents/ml/MTensor;->getData()[F

    move-result-object v5

    const/4 v7, 0x0

    :goto_0
    if-ge v7, v4, :cond_4

    const/4 v11, 0x0

    :goto_1
    if-ge v11, v8, :cond_3

    const/4 v12, 0x0

    :goto_2
    if-ge v12, v9, :cond_2

    mul-int v13, v7, v9

    mul-int v13, v13, v8

    mul-int v14, v12, v8

    add-int/2addr v13, v14

    add-int/2addr v13, v11

    mul-int v15, v7, v6

    mul-int v15, v15, v8

    add-int/2addr v15, v14

    add-int/2addr v15, v11

    const/4 v14, 0x1

    .line 247
    aput v14, v5, v13

    const/4 v14, 0x0

    :goto_3
    if-ge v14, v1, :cond_1

    .line 249
    aget v2, v5, v13

    mul-int v16, v14, v8

    add-int v16, v15, v16

    aget v3, v0, v16

    invoke-static {v2, v3}, Ljava/lang/Math;->max(FF)F

    move-result v2

    aput v2, v5, v13
    :try_end_0
    .catch Ljava/lang/Throwable; {:try_start_0 .. :try_end_0} :catch_0

    add-int/lit8 v14, v14, 0x1

    const/4 v2, 0x0

    const/4 v3, 0x0

    goto :goto_3

    :cond_1
    add-int/lit8 v12, v12, 0x1

    const/4 v2, 0x0

    const/4 v3, 0x0

    goto :goto_2

    :cond_2
    add-int/lit8 v11, v11, 0x1

    const/4 v2, 0x0

    const/4 v3, 0x0

    goto :goto_1

    :cond_3
    add-int/lit8 v7, v7, 0x1

    const/4 v2, 0x0

    const/4 v3, 0x0

    goto :goto_0

    :cond_4
    return-object v10

    :catch_0
    move-exception v0

    .line 254
    const-class v1, Lcom/facebook/appevents/ml/Operator;

    invoke-static {v0, v1}, Lcom/facebook/internal/instrument/crashshield/CrashShieldHandler;->handleThrowable(Ljava/lang/Throwable;Ljava/lang/Object;)V

    const/4 v1, 0x0

    return-object v1
.end method

.method static mul(Lcom/facebook/appevents/ml/MTensor;Lcom/facebook/appevents/ml/MTensor;)Lcom/facebook/appevents/ml/MTensor;
    .locals 14

    const-class v0, Lcom/facebook/appevents/ml/Operator;

    invoke-static {v0}, Lcom/facebook/internal/instrument/crashshield/CrashShieldHandler;->isObjectCrashing(Ljava/lang/Object;)Z

    move-result v0

    const/4 v1, 0x0

    if-eqz v0, :cond_0

    return-object v1

    :cond_0
    const/4 v0, 0x0

    .line 45
    :try_start_0
    invoke-virtual {p0, v0}, Lcom/facebook/appevents/ml/MTensor;->getShape(I)I

    move-result v2

    .line 46
    invoke-virtual {p1, v0}, Lcom/facebook/appevents/ml/MTensor;->getShape(I)I

    move-result v3

    const/4 v4, 0x1

    .line 47
    invoke-virtual {p1, v4}, Lcom/facebook/appevents/ml/MTensor;->getShape(I)I

    move-result v5

    .line 48
    new-instance v6, Lcom/facebook/appevents/ml/MTensor;

    const/4 v7, 0x2

    new-array v7, v7, [I

    aput v2, v7, v0

    aput v5, v7, v4

    invoke-direct {v6, v7}, Lcom/facebook/appevents/ml/MTensor;-><init>([I)V

    .line 49
    invoke-virtual {p0}, Lcom/facebook/appevents/ml/MTensor;->getData()[F

    move-result-object p0

    .line 50
    invoke-virtual {p1}, Lcom/facebook/appevents/ml/MTensor;->getData()[F

    move-result-object p1

    .line 51
    invoke-virtual {v6}, Lcom/facebook/appevents/ml/MTensor;->getData()[F

    move-result-object v4

    const/4 v7, 0x0

    :goto_0
    if-ge v7, v2, :cond_3

    const/4 v8, 0x0

    :goto_1
    if-ge v8, v5, :cond_2

    mul-int v9, v7, v5

    add-int/2addr v9, v8

    const/4 v10, 0x0

    .line 55
    aput v10, v4, v9

    const/4 v10, 0x0

    :goto_2
    if-ge v10, v3, :cond_1

    .line 57
    aget v11, v4, v9

    mul-int v12, v7, v3

    add-int/2addr v12, v10

    aget v12, p0, v12

    mul-int v13, v10, v5

    add-int/2addr v13, v8

    aget v13, p1, v13

    mul-float v12, v12, v13

    add-float/2addr v11, v12

    aput v11, v4, v9
    :try_end_0
    .catch Ljava/lang/Throwable; {:try_start_0 .. :try_end_0} :catch_0

    add-int/lit8 v10, v10, 0x1

    goto :goto_2

    :cond_1
    add-int/lit8 v8, v8, 0x1

    goto :goto_1

    :cond_2
    add-int/lit8 v7, v7, 0x1

    goto :goto_0

    :cond_3
    return-object v6

    :catch_0
    move-exception p0

    .line 61
    const-class p1, Lcom/facebook/appevents/ml/Operator;

    invoke-static {p0, p1}, Lcom/facebook/internal/instrument/crashshield/CrashShieldHandler;->handleThrowable(Ljava/lang/Throwable;Ljava/lang/Object;)V

    return-object v1
.end method

.method static relu(Lcom/facebook/appevents/ml/MTensor;)V
    .locals 3

    const-class v0, Lcom/facebook/appevents/ml/Operator;

    invoke-static {v0}, Lcom/facebook/internal/instrument/crashshield/CrashShieldHandler;->isObjectCrashing(Ljava/lang/Object;)Z

    move-result v0

    if-eqz v0, :cond_0

    return-void

    .line 65
    :cond_0
    :try_start_0
    invoke-virtual {p0}, Lcom/facebook/appevents/ml/MTensor;->getData()[F

    move-result-object p0

    const/4 v0, 0x0

    .line 66
    :goto_0
    array-length v1, p0

    if-ge v0, v1, :cond_2

    .line 67
    aget v1, p0, v0

    const/4 v2, 0x0

    cmpg-float v1, v1, v2

    if-gez v1, :cond_1

    .line 68
    aput v2, p0, v0
    :try_end_0
    .catch Ljava/lang/Throwable; {:try_start_0 .. :try_end_0} :catch_0

    :cond_1
    add-int/lit8 v0, v0, 0x1

    goto :goto_0

    :cond_2
    return-void

    :catch_0
    move-exception p0

    .line 71
    const-class v0, Lcom/facebook/appevents/ml/Operator;

    invoke-static {p0, v0}, Lcom/facebook/internal/instrument/crashshield/CrashShieldHandler;->handleThrowable(Ljava/lang/Throwable;Ljava/lang/Object;)V

    return-void
.end method

.method static softmax(Lcom/facebook/appevents/ml/MTensor;)V
    .locals 10

    const-class v0, Lcom/facebook/appevents/ml/Operator;

    invoke-static {v0}, Lcom/facebook/internal/instrument/crashshield/CrashShieldHandler;->isObjectCrashing(Ljava/lang/Object;)Z

    move-result v0

    if-eqz v0, :cond_0

    return-void

    :cond_0
    const/4 v0, 0x0

    .line 111
    :try_start_0
    invoke-virtual {p0, v0}, Lcom/facebook/appevents/ml/MTensor;->getShape(I)I

    move-result v1

    const/4 v2, 0x1

    .line 112
    invoke-virtual {p0, v2}, Lcom/facebook/appevents/ml/MTensor;->getShape(I)I

    move-result v2

    .line 113
    invoke-virtual {p0}, Lcom/facebook/appevents/ml/MTensor;->getData()[F

    move-result-object p0

    :goto_0
    if-ge v0, v1, :cond_6

    mul-int v3, v0, v2

    add-int v4, v3, v2

    const/4 v5, 0x1

    const/4 v6, 0x0

    move v5, v3

    const/4 v7, 0x1

    :goto_1
    if-ge v5, v4, :cond_2

    .line 121
    aget v8, p0, v5

    cmpl-float v8, v8, v7

    if-lez v8, :cond_1

    .line 122
    aget v7, p0, v5

    :cond_1
    add-int/lit8 v5, v5, 0x1

    goto :goto_1

    :cond_2
    move v5, v3

    :goto_2
    if-ge v5, v4, :cond_3

    .line 127
    aget v8, p0, v5

    sub-float/2addr v8, v7

    float-to-double v8, v8

    invoke-static {v8, v9}, Ljava/lang/Math;->exp(D)D

    move-result-wide v8

    double-to-float v8, v8

    aput v8, p0, v5

    add-int/lit8 v5, v5, 0x1

    goto :goto_2

    :cond_3
    move v5, v3

    :goto_3
    if-ge v5, v4, :cond_4

    .line 131
    aget v7, p0, v5

    add-float/2addr v6, v7

    add-int/lit8 v5, v5, 0x1

    goto :goto_3

    :cond_4
    :goto_4
    if-ge v3, v4, :cond_5

    .line 135
    aget v5, p0, v3

    div-float/2addr v5, v6

    aput v5, p0, v3
    :try_end_0
    .catch Ljava/lang/Throwable; {:try_start_0 .. :try_end_0} :catch_0

    add-int/lit8 v3, v3, 0x1

    goto :goto_4

    :cond_5
    add-int/lit8 v0, v0, 0x1

    goto :goto_0

    :cond_6
    return-void

    :catch_0
    move-exception p0

    .line 138
    const-class v0, Lcom/facebook/appevents/ml/Operator;

    invoke-static {p0, v0}, Lcom/facebook/internal/instrument/crashshield/CrashShieldHandler;->handleThrowable(Ljava/lang/Throwable;Ljava/lang/Object;)V

    return-void
.end method

.method static transpose2D(Lcom/facebook/appevents/ml/MTensor;)Lcom/facebook/appevents/ml/MTensor;
    .locals 10

    const-class v0, Lcom/facebook/appevents/ml/Operator;

    invoke-static {v0}, Lcom/facebook/internal/instrument/crashshield/CrashShieldHandler;->isObjectCrashing(Ljava/lang/Object;)Z

    move-result v0

    const/4 v1, 0x0

    if-eqz v0, :cond_0

    return-object v1

    :cond_0
    const/4 v0, 0x0

    .line 172
    :try_start_0
    invoke-virtual {p0, v0}, Lcom/facebook/appevents/ml/MTensor;->getShape(I)I

    move-result v2

    const/4 v3, 0x1

    .line 173
    invoke-virtual {p0, v3}, Lcom/facebook/appevents/ml/MTensor;->getShape(I)I

    move-result v4

    .line 174
    new-instance v5, Lcom/facebook/appevents/ml/MTensor;

    const/4 v6, 0x2

    new-array v6, v6, [I

    aput v4, v6, v0

    aput v2, v6, v3

    invoke-direct {v5, v6}, Lcom/facebook/appevents/ml/MTensor;-><init>([I)V

    .line 175
    invoke-virtual {p0}, Lcom/facebook/appevents/ml/MTensor;->getData()[F

    move-result-object p0

    .line 176
    invoke-virtual {v5}, Lcom/facebook/appevents/ml/MTensor;->getData()[F

    move-result-object v3

    const/4 v6, 0x0

    :goto_0
    if-ge v6, v2, :cond_2

    const/4 v7, 0x0

    :goto_1
    if-ge v7, v4, :cond_1

    mul-int v8, v7, v2

    add-int/2addr v8, v6

    mul-int v9, v6, v4

    add-int/2addr v9, v7

    .line 180
    aget v9, p0, v9

    aput v9, v3, v8
    :try_end_0
    .catch Ljava/lang/Throwable; {:try_start_0 .. :try_end_0} :catch_0

    add-int/lit8 v7, v7, 0x1

    goto :goto_1

    :cond_1
    add-int/lit8 v6, v6, 0x1

    goto :goto_0

    :cond_2
    return-object v5

    :catch_0
    move-exception p0

    .line 183
    const-class v0, Lcom/facebook/appevents/ml/Operator;

    invoke-static {p0, v0}, Lcom/facebook/internal/instrument/crashshield/CrashShieldHandler;->handleThrowable(Ljava/lang/Throwable;Ljava/lang/Object;)V

    return-object v1
.end method

.method static transpose3D(Lcom/facebook/appevents/ml/MTensor;)Lcom/facebook/appevents/ml/MTensor;
    .locals 13

    const-class v0, Lcom/facebook/appevents/ml/Operator;

    invoke-static {v0}, Lcom/facebook/internal/instrument/crashshield/CrashShieldHandler;->isObjectCrashing(Ljava/lang/Object;)Z

    move-result v0

    const/4 v1, 0x0

    if-eqz v0, :cond_0

    return-object v1

    :cond_0
    const/4 v0, 0x0

    .line 187
    :try_start_0
    invoke-virtual {p0, v0}, Lcom/facebook/appevents/ml/MTensor;->getShape(I)I

    move-result v2

    const/4 v3, 0x1

    .line 188
    invoke-virtual {p0, v3}, Lcom/facebook/appevents/ml/MTensor;->getShape(I)I

    move-result v4

    const/4 v5, 0x2

    .line 189
    invoke-virtual {p0, v5}, Lcom/facebook/appevents/ml/MTensor;->getShape(I)I

    move-result v6

    .line 190
    new-instance v7, Lcom/facebook/appevents/ml/MTensor;

    const/4 v8, 0x3

    new-array v8, v8, [I

    aput v6, v8, v0

    aput v4, v8, v3

    aput v2, v8, v5

    invoke-direct {v7, v8}, Lcom/facebook/appevents/ml/MTensor;-><init>([I)V

    .line 191
    invoke-virtual {p0}, Lcom/facebook/appevents/ml/MTensor;->getData()[F

    move-result-object p0

    .line 192
    invoke-virtual {v7}, Lcom/facebook/appevents/ml/MTensor;->getData()[F

    move-result-object v3

    const/4 v5, 0x0

    :goto_0
    if-ge v5, v2, :cond_3

    const/4 v8, 0x0

    :goto_1
    if-ge v8, v4, :cond_2

    const/4 v9, 0x0

    :goto_2
    if-ge v9, v6, :cond_1

    mul-int v10, v9, v2

    mul-int v10, v10, v4

    mul-int v11, v8, v2

    add-int/2addr v10, v11

    add-int/2addr v10, v5

    mul-int v11, v5, v4

    mul-int v11, v11, v6

    mul-int v12, v8, v6

    add-int/2addr v11, v12

    add-int/2addr v11, v9

    .line 197
    aget v11, p0, v11

    aput v11, v3, v10
    :try_end_0
    .catch Ljava/lang/Throwable; {:try_start_0 .. :try_end_0} :catch_0

    add-int/lit8 v9, v9, 0x1

    goto :goto_2

    :cond_1
    add-int/lit8 v8, v8, 0x1

    goto :goto_1

    :cond_2
    add-int/lit8 v5, v5, 0x1

    goto :goto_0

    :cond_3
    return-object v7

    :catch_0
    move-exception p0

    .line 201
    const-class v0, Lcom/facebook/appevents/ml/Operator;

    invoke-static {p0, v0}, Lcom/facebook/internal/instrument/crashshield/CrashShieldHandler;->handleThrowable(Ljava/lang/Throwable;Ljava/lang/Object;)V

    return-object v1
.end method
