.class abstract Lcom/zopim/android/sdk/api/i;
.super Ljava/lang/Object;

# interfaces
.implements Ljava/io/Serializable;


# annotations
.annotation system Ldalvik/annotation/Signature;
    value = {
        "<T:",
        "Lcom/zopim/android/sdk/api/i;",
        ">",
        "Ljava/lang/Object;",
        "Ljava/io/Serializable;"
    }
.end annotation


# static fields
.field private static final LOG_TAG:Ljava/lang/String; = "i"

.field private static final serialVersionUID:J = 0x5d8ffe837f4ba6bcL


# instance fields
.field department:Ljava/lang/String;

.field preChatForm:Lcom/zopim/android/sdk/prechat/PreChatForm;

.field referrer:Ljava/lang/String;

.field tags:[Ljava/lang/String;

.field title:Ljava/lang/String;


# direct methods
.method static constructor <clinit>()V
    .locals 0

    return-void
.end method

.method constructor <init>()V
    .locals 0

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public department(Ljava/lang/String;)Lcom/zopim/android/sdk/api/i;
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/lang/String;",
            ")TT;"
        }
    .end annotation

    if-eqz p1, :cond_1

    invoke-virtual {p1}, Ljava/lang/String;->isEmpty()Z

    move-result v0

    if-eqz v0, :cond_0

    goto :goto_0

    :cond_0
    iput-object p1, p0, Lcom/zopim/android/sdk/api/i;->department:Ljava/lang/String;

    goto :goto_1

    :cond_1
    :goto_0
    sget-object p1, Lcom/zopim/android/sdk/api/i;->LOG_TAG:Ljava/lang/String;

    const-string v0, "Minimum department validation failed. Can not be null or empty string"

    invoke-static {p1, v0}, Landroid/util/Log;->w(Ljava/lang/String;Ljava/lang/String;)I

    :goto_1
    return-object p0
.end method

.method public preChatForm(Lcom/zopim/android/sdk/prechat/PreChatForm;)Lcom/zopim/android/sdk/api/i;
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Lcom/zopim/android/sdk/prechat/PreChatForm;",
            ")TT;"
        }
    .end annotation

    if-nez p1, :cond_0

    sget-object p1, Lcom/zopim/android/sdk/api/i;->LOG_TAG:Ljava/lang/String;

    const-string v0, "PreChatForm must not be null"

    invoke-static {p1, v0}, Landroid/util/Log;->w(Ljava/lang/String;Ljava/lang/String;)I

    goto :goto_0

    :cond_0
    iput-object p1, p0, Lcom/zopim/android/sdk/api/i;->preChatForm:Lcom/zopim/android/sdk/prechat/PreChatForm;

    :goto_0
    return-object p0
.end method

.method public varargs tags([Ljava/lang/String;)Lcom/zopim/android/sdk/api/i;
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "([",
            "Ljava/lang/String;",
            ")TT;"
        }
    .end annotation

    if-nez p1, :cond_0

    sget-object p1, Lcom/zopim/android/sdk/api/i;->LOG_TAG:Ljava/lang/String;

    const-string v0, "Tags must not be null or empty string"

    invoke-static {p1, v0}, Landroid/util/Log;->w(Ljava/lang/String;Ljava/lang/String;)I

    goto :goto_0

    :cond_0
    iput-object p1, p0, Lcom/zopim/android/sdk/api/i;->tags:[Ljava/lang/String;

    :goto_0
    return-object p0
.end method

.method public visitorPathOne(Ljava/lang/String;)Lcom/zopim/android/sdk/api/i;
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/lang/String;",
            ")TT;"
        }
    .end annotation

    if-eqz p1, :cond_1

    invoke-virtual {p1}, Ljava/lang/String;->isEmpty()Z

    move-result v0

    if-eqz v0, :cond_0

    goto :goto_0

    :cond_0
    iput-object p1, p0, Lcom/zopim/android/sdk/api/i;->title:Ljava/lang/String;

    goto :goto_1

    :cond_1
    :goto_0
    sget-object p1, Lcom/zopim/android/sdk/api/i;->LOG_TAG:Ljava/lang/String;

    const-string v0, "Visitor path must not be null or empty string"

    invoke-static {p1, v0}, Landroid/util/Log;->w(Ljava/lang/String;Ljava/lang/String;)I

    :goto_1
    return-object p0
.end method

.method public visitorPathTwo(Ljava/lang/String;)Lcom/zopim/android/sdk/api/i;
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/lang/String;",
            ")TT;"
        }
    .end annotation

    if-eqz p1, :cond_1

    invoke-virtual {p1}, Ljava/lang/String;->isEmpty()Z

    move-result v0

    if-eqz v0, :cond_0

    goto :goto_0

    :cond_0
    iput-object p1, p0, Lcom/zopim/android/sdk/api/i;->referrer:Ljava/lang/String;

    goto :goto_1

    :cond_1
    :goto_0
    sget-object p1, Lcom/zopim/android/sdk/api/i;->LOG_TAG:Ljava/lang/String;

    const-string v0, "Visitor path must not be null or empty string"

    invoke-static {p1, v0}, Landroid/util/Log;->w(Ljava/lang/String;Ljava/lang/String;)I

    :goto_1
    return-object p0
.end method
