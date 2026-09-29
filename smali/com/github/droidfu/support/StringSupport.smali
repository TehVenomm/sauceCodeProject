.class public Lcom/github/droidfu/support/StringSupport;
.super Ljava/lang/Object;
.source "StringSupport.java"


# direct methods
.method public constructor <init>()V
    .locals 0

    .line 8
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method

.method private static splitByCharacterType(Ljava/lang/String;Z)[Ljava/lang/String;
    .locals 8

    if-nez p0, :cond_0

    const/4 p0, 0x0

    return-object p0

    .line 80
    :cond_0
    invoke-virtual {p0}, Ljava/lang/String;->length()I

    move-result v0

    const/4 v1, 0x0

    if-nez v0, :cond_1

    .line 81
    new-array p0, v1, [Ljava/lang/String;

    return-object p0

    .line 83
    :cond_1
    invoke-virtual {p0}, Ljava/lang/String;->toCharArray()[C

    move-result-object p0

    .line 84
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    .line 86
    aget-char v2, p0, v1

    invoke-static {v2}, Ljava/lang/Character;->getType(C)I

    move-result v2

    const/4 v3, 0x1

    move v4, v2

    const/4 v1, 0x1

    const/4 v2, 0x0

    .line 87
    :goto_0
    array-length v5, p0

    if-lt v1, v5, :cond_2

    .line 105
    new-instance p1, Ljava/lang/String;

    array-length v1, p0

    sub-int/2addr v1, v2

    invoke-direct {p1, p0, v2, v1}, Ljava/lang/String;-><init>([CII)V

    invoke-virtual {v0, p1}, Ljava/util/ArrayList;->add(Ljava/lang/Object;)Z

    .line 106
    invoke-virtual {v0}, Ljava/util/ArrayList;->size()I

    move-result p0

    new-array p0, p0, [Ljava/lang/String;

    invoke-virtual {v0, p0}, Ljava/util/ArrayList;->toArray([Ljava/lang/Object;)[Ljava/lang/Object;

    move-result-object p0

    check-cast p0, [Ljava/lang/String;

    return-object p0

    .line 88
    :cond_2
    aget-char v5, p0, v1

    invoke-static {v5}, Ljava/lang/Character;->getType(C)I

    move-result v5

    if-ne v5, v4, :cond_3

    goto :goto_2

    :cond_3
    if-eqz p1, :cond_4

    const/4 v6, 0x2

    if-ne v5, v6, :cond_4

    if-ne v4, v3, :cond_4

    add-int/lit8 v4, v1, -0x1

    if-eq v4, v2, :cond_5

    .line 96
    new-instance v6, Ljava/lang/String;

    sub-int v7, v4, v2

    invoke-direct {v6, p0, v2, v7}, Ljava/lang/String;-><init>([CII)V

    invoke-virtual {v0, v6}, Ljava/util/ArrayList;->add(Ljava/lang/Object;)Z

    move v2, v4

    goto :goto_1

    .line 100
    :cond_4
    new-instance v4, Ljava/lang/String;

    sub-int v6, v1, v2

    invoke-direct {v4, p0, v2, v6}, Ljava/lang/String;-><init>([CII)V

    invoke-virtual {v0, v4}, Ljava/util/ArrayList;->add(Ljava/lang/Object;)Z

    move v2, v1

    :cond_5
    :goto_1
    move v4, v5

    :goto_2
    add-int/lit8 v1, v1, 0x1

    goto :goto_0
.end method

.method public static splitByCharacterTypeCamelCase(Ljava/lang/String;)[Ljava/lang/String;
    .locals 1

    const/4 v0, 0x1

    .line 53
    invoke-static {p0, v0}, Lcom/github/droidfu/support/StringSupport;->splitByCharacterType(Ljava/lang/String;Z)[Ljava/lang/String;

    move-result-object p0

    return-object p0
.end method

.method public static underscore(Ljava/lang/String;)Ljava/lang/String;
    .locals 1

    .line 19
    invoke-static {p0}, Lcom/github/droidfu/support/StringSupport;->splitByCharacterTypeCamelCase(Ljava/lang/String;)[Ljava/lang/String;

    move-result-object p0

    const-string v0, "_"

    .line 20
    invoke-static {v0, p0}, Landroid/text/TextUtils;->join(Ljava/lang/CharSequence;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object p0

    invoke-virtual {p0}, Ljava/lang/String;->toLowerCase()Ljava/lang/String;

    move-result-object p0

    return-object p0
.end method
