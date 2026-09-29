.class public Ljp/colopl/util/MailTo;
.super Ljava/lang/Object;
.source "MailTo.java"


# static fields
.field private static final BODY:Ljava/lang/String; = "body"

.field private static final CC:Ljava/lang/String; = "cc"

.field public static final MAILTO_SCHEME:Ljava/lang/String; = "mailto:"

.field private static final SUBJECT:Ljava/lang/String; = "subject"

.field private static final TO:Ljava/lang/String; = "to"


# instance fields
.field private mHeaders:Ljava/util/HashMap;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/HashMap<",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            ">;"
        }
    .end annotation
.end field


# direct methods
.method private constructor <init>()V
    .locals 1

    .line 144
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 145
    new-instance v0, Ljava/util/HashMap;

    invoke-direct {v0}, Ljava/util/HashMap;-><init>()V

    iput-object v0, p0, Ljp/colopl/util/MailTo;->mHeaders:Ljava/util/HashMap;

    return-void
.end method

.method public static isMailTo(Ljava/lang/String;)Z
    .locals 1

    if-eqz p0, :cond_0

    const-string v0, "mailto:"

    .line 33
    invoke-virtual {p0, v0}, Ljava/lang/String;->startsWith(Ljava/lang/String;)Z

    move-result p0

    if-eqz p0, :cond_0

    const/4 p0, 0x1

    return p0

    :cond_0
    const/4 p0, 0x0

    return p0
.end method

.method public static parse(Ljava/lang/String;)Ljp/colopl/util/MailTo;
    .locals 10
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/lang/RuntimeException;
        }
    .end annotation

    if-eqz p0, :cond_6

    .line 43
    invoke-static {p0}, Ljp/colopl/util/MailTo;->isMailTo(Ljava/lang/String;)Z

    move-result v0

    if-eqz v0, :cond_5

    const-string v0, "mailto:"

    .line 47
    invoke-virtual {v0}, Ljava/lang/String;->length()I

    move-result v0

    invoke-virtual {p0, v0}, Ljava/lang/String;->substring(I)Ljava/lang/String;

    move-result-object p0

    .line 48
    invoke-static {p0}, Landroid/net/Uri;->parse(Ljava/lang/String;)Landroid/net/Uri;

    move-result-object p0

    .line 49
    new-instance v0, Ljp/colopl/util/MailTo;

    invoke-direct {v0}, Ljp/colopl/util/MailTo;-><init>()V

    .line 52
    invoke-virtual {p0}, Landroid/net/Uri;->getEncodedQuery()Ljava/lang/String;

    move-result-object v1

    if-eqz v1, :cond_2

    const-string v2, "&"

    .line 54
    invoke-virtual {v1, v2}, Ljava/lang/String;->split(Ljava/lang/String;)[Ljava/lang/String;

    move-result-object v1

    .line 55
    array-length v2, v1

    const/4 v3, 0x0

    const/4 v4, 0x0

    :goto_0
    if-ge v4, v2, :cond_2

    aget-object v5, v1, v4

    const-string v6, "="

    .line 56
    invoke-virtual {v5, v6}, Ljava/lang/String;->split(Ljava/lang/String;)[Ljava/lang/String;

    move-result-object v5

    .line 57
    array-length v6, v5

    if-nez v6, :cond_0

    goto :goto_2

    .line 62
    :cond_0
    iget-object v6, v0, Ljp/colopl/util/MailTo;->mHeaders:Ljava/util/HashMap;

    aget-object v7, v5, v3

    invoke-static {v7}, Landroid/net/Uri;->decode(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v7

    invoke-virtual {v7}, Ljava/lang/String;->toLowerCase()Ljava/lang/String;

    move-result-object v7

    array-length v8, v5

    const/4 v9, 0x1

    if-le v8, v9, :cond_1

    aget-object v5, v5, v9

    .line 63
    invoke-static {v5}, Landroid/net/Uri;->decode(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v5

    goto :goto_1

    :cond_1
    const/4 v5, 0x0

    .line 62
    :goto_1
    invoke-virtual {v6, v7, v5}, Ljava/util/HashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    :goto_2
    add-int/lit8 v4, v4, 0x1

    goto :goto_0

    .line 69
    :cond_2
    invoke-virtual {p0}, Landroid/net/Uri;->getPath()Ljava/lang/String;

    move-result-object p0

    if-eqz p0, :cond_4

    .line 71
    invoke-virtual {v0}, Ljp/colopl/util/MailTo;->getTo()Ljava/lang/String;

    move-result-object v1

    if-eqz v1, :cond_3

    .line 73
    new-instance v2, Ljava/lang/StringBuilder;

    invoke-direct {v2}, Ljava/lang/StringBuilder;-><init>()V

    invoke-virtual {v2, p0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string p0, ", "

    invoke-virtual {v2, p0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v2, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v2}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p0

    .line 75
    :cond_3
    iget-object v1, v0, Ljp/colopl/util/MailTo;->mHeaders:Ljava/util/HashMap;

    const-string v2, "to"

    invoke-virtual {v1, v2, p0}, Ljava/util/HashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    :cond_4
    return-object v0

    .line 44
    :cond_5
    new-instance p0, Ljava/lang/RuntimeException;

    const-string v0, "Not a mailto scheme"

    invoke-direct {p0, v0}, Ljava/lang/RuntimeException;-><init>(Ljava/lang/String;)V

    throw p0

    .line 41
    :cond_6
    new-instance p0, Ljava/lang/NullPointerException;

    invoke-direct {p0}, Ljava/lang/NullPointerException;-><init>()V

    throw p0
.end method


# virtual methods
.method public getBody()Ljava/lang/String;
    .locals 2

    .line 116
    iget-object v0, p0, Ljp/colopl/util/MailTo;->mHeaders:Ljava/util/HashMap;

    const-string v1, "body"

    invoke-virtual {v0, v1}, Ljava/util/HashMap;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Ljava/lang/String;

    return-object v0
.end method

.method public getCc()Ljava/lang/String;
    .locals 2

    .line 98
    iget-object v0, p0, Ljp/colopl/util/MailTo;->mHeaders:Ljava/util/HashMap;

    const-string v1, "cc"

    invoke-virtual {v0, v1}, Ljava/util/HashMap;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Ljava/lang/String;

    return-object v0
.end method

.method public getHeaders()Ljava/util/Map;
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()",
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            ">;"
        }
    .end annotation

    .line 124
    iget-object v0, p0, Ljp/colopl/util/MailTo;->mHeaders:Ljava/util/HashMap;

    return-object v0
.end method

.method public getSubject()Ljava/lang/String;
    .locals 2

    .line 107
    iget-object v0, p0, Ljp/colopl/util/MailTo;->mHeaders:Ljava/util/HashMap;

    const-string v1, "subject"

    invoke-virtual {v0, v1}, Ljava/util/HashMap;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Ljava/lang/String;

    return-object v0
.end method

.method public getTo()Ljava/lang/String;
    .locals 2

    .line 88
    iget-object v0, p0, Ljp/colopl/util/MailTo;->mHeaders:Ljava/util/HashMap;

    const-string v1, "to"

    invoke-virtual {v0, v1}, Ljava/util/HashMap;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Ljava/lang/String;

    return-object v0
.end method

.method public toString()Ljava/lang/String;
    .locals 4

    .line 129
    new-instance v0, Ljava/lang/StringBuilder;

    const-string v1, "mailto:"

    invoke-direct {v0, v1}, Ljava/lang/StringBuilder;-><init>(Ljava/lang/String;)V

    const/16 v1, 0x3f

    .line 130
    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    .line 131
    iget-object v1, p0, Ljp/colopl/util/MailTo;->mHeaders:Ljava/util/HashMap;

    invoke-virtual {v1}, Ljava/util/HashMap;->entrySet()Ljava/util/Set;

    move-result-object v1

    invoke-interface {v1}, Ljava/util/Set;->iterator()Ljava/util/Iterator;

    move-result-object v1

    :goto_0
    invoke-interface {v1}, Ljava/util/Iterator;->hasNext()Z

    move-result v2

    if-eqz v2, :cond_0

    invoke-interface {v1}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Ljava/util/Map$Entry;

    .line 132
    invoke-interface {v2}, Ljava/util/Map$Entry;->getKey()Ljava/lang/Object;

    move-result-object v3

    check-cast v3, Ljava/lang/String;

    invoke-static {v3}, Landroid/net/Uri;->encode(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v3

    invoke-virtual {v0, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const/16 v3, 0x3d

    .line 133
    invoke-virtual {v0, v3}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    .line 134
    invoke-interface {v2}, Ljava/util/Map$Entry;->getValue()Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Ljava/lang/String;

    invoke-static {v2}, Landroid/net/Uri;->encode(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v0, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const/16 v2, 0x26

    .line 135
    invoke-virtual {v0, v2}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    goto :goto_0

    .line 137
    :cond_0
    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v0

    return-object v0
.end method
