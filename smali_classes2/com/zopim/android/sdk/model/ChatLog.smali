.class public Lcom/zopim/android/sdk/model/ChatLog;
.super Ljava/lang/Object;

# interfaces
.implements Ljava/lang/Comparable;


# annotations
.annotation runtime Lcom/fasterxml/jackson/annotation/JsonIgnoreProperties;
    ignoreUnknown = true
.end annotation

.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Lcom/zopim/android/sdk/model/ChatLog$Option;,
        Lcom/zopim/android/sdk/model/ChatLog$Error;,
        Lcom/zopim/android/sdk/model/ChatLog$a;,
        Lcom/zopim/android/sdk/model/ChatLog$b;,
        Lcom/zopim/android/sdk/model/ChatLog$Type;,
        Lcom/zopim/android/sdk/model/ChatLog$Rating;
    }
.end annotation

.annotation system Ldalvik/annotation/Signature;
    value = {
        "Ljava/lang/Object;",
        "Ljava/lang/Comparable<",
        "Lcom/zopim/android/sdk/model/ChatLog;",
        ">;"
    }
.end annotation


# static fields
.field private static final LOG_TAG:Ljava/lang/String; = "ChatLog"
    .annotation runtime Lcom/fasterxml/jackson/annotation/JsonIgnore;
    .end annotation
.end field


# instance fields
.field private attachment:Lcom/zopim/android/sdk/model/Attachment;
    .annotation runtime Lcom/fasterxml/jackson/annotation/JsonProperty;
        value = "attachment"
    .end annotation
.end field

.field private comment:Ljava/lang/String;
    .annotation runtime Lcom/fasterxml/jackson/annotation/JsonProperty;
        value = "new_comment$string"
    .end annotation
.end field

.field private displayName:Ljava/lang/String;
    .annotation runtime Lcom/fasterxml/jackson/annotation/JsonProperty;
        value = "display_name$string"
    .end annotation
.end field

.field private error:Ljava/lang/String;
    .annotation runtime Lcom/fasterxml/jackson/annotation/JsonProperty;
        value = "error$string"
    .end annotation
.end field

.field private failed:Ljava/lang/Boolean;
    .annotation runtime Lcom/fasterxml/jackson/annotation/JsonProperty;
        value = "failed$bool"
    .end annotation
.end field

.field private file:Ljava/io/File;
    .annotation runtime Lcom/fasterxml/jackson/annotation/JsonIgnore;
    .end annotation
.end field

.field private fileName:Ljava/lang/String;
    .annotation runtime Lcom/fasterxml/jackson/annotation/JsonProperty;
        value = "file_name$string"
    .end annotation
.end field

.field private fileSize:Ljava/lang/Integer;
    .annotation runtime Lcom/fasterxml/jackson/annotation/JsonProperty;
        value = "file_size$int"
    .end annotation
.end field

.field private fileType:Ljava/lang/String;
    .annotation runtime Lcom/fasterxml/jackson/annotation/JsonProperty;
        value = "file_type$string"
    .end annotation
.end field

.field private message:Ljava/lang/String;
    .annotation runtime Lcom/fasterxml/jackson/annotation/JsonProperty;
        value = "msg$string"
    .end annotation
.end field

.field private nick:Ljava/lang/String;
    .annotation runtime Lcom/fasterxml/jackson/annotation/JsonProperty;
        value = "nick$string"
    .end annotation
.end field

.field private options:[Lcom/zopim/android/sdk/model/ChatLog$Option;
    .annotation runtime Lcom/fasterxml/jackson/annotation/JsonIgnore;
    .end annotation
.end field

.field private rating:Ljava/lang/String;
    .annotation runtime Lcom/fasterxml/jackson/annotation/JsonProperty;
        value = "new_rating$string"
    .end annotation
.end field

.field private timestamp:Ljava/lang/Long;
    .annotation runtime Lcom/fasterxml/jackson/annotation/JsonProperty;
        value = "timestamp$int"
    .end annotation
.end field

.field private type:Ljava/lang/String;
    .annotation runtime Lcom/fasterxml/jackson/annotation/JsonProperty;
        value = "type$string"
    .end annotation
.end field

.field private unverified:Ljava/lang/Boolean;
    .annotation runtime Lcom/fasterxml/jackson/annotation/JsonProperty;
        value = "unverified$bool"
    .end annotation
.end field

.field private uploadProgress:I
    .annotation runtime Lcom/fasterxml/jackson/annotation/JsonIgnore;
    .end annotation
.end field

.field private uploadUrl:Ljava/lang/String;
    .annotation runtime Lcom/fasterxml/jackson/annotation/JsonProperty;
        value = "post_url$string"
    .end annotation
.end field

.field private visitorQueue:Ljava/lang/Integer;
    .annotation runtime Lcom/fasterxml/jackson/annotation/JsonProperty;
        value = "visitor_queue$int"
    .end annotation
.end field


# direct methods
.method static constructor <clinit>()V
    .locals 0

    return-void
.end method

.method public constructor <init>()V
    .locals 1

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    const/4 v0, 0x0

    iput v0, p0, Lcom/zopim/android/sdk/model/ChatLog;->uploadProgress:I

    return-void
.end method

.method public constructor <init>(Ljava/lang/String;Lcom/zopim/android/sdk/model/ChatLog$Type;Ljava/lang/String;)V
    .locals 3

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    const/4 v0, 0x0

    iput v0, p0, Lcom/zopim/android/sdk/model/ChatLog;->uploadProgress:I

    invoke-static {}, Ljava/lang/System;->currentTimeMillis()J

    move-result-wide v1

    invoke-static {v1, v2}, Ljava/lang/Long;->valueOf(J)Ljava/lang/Long;

    move-result-object v1

    iput-object v1, p0, Lcom/zopim/android/sdk/model/ChatLog;->timestamp:Ljava/lang/Long;

    iput-object p1, p0, Lcom/zopim/android/sdk/model/ChatLog;->displayName:Ljava/lang/String;

    iput-object p3, p0, Lcom/zopim/android/sdk/model/ChatLog;->message:Ljava/lang/String;

    const/4 p1, 0x1

    invoke-static {p1}, Ljava/lang/Boolean;->valueOf(Z)Ljava/lang/Boolean;

    move-result-object p1

    iput-object p1, p0, Lcom/zopim/android/sdk/model/ChatLog;->unverified:Ljava/lang/Boolean;

    invoke-static {v0}, Ljava/lang/Boolean;->valueOf(Z)Ljava/lang/Boolean;

    move-result-object p1

    iput-object p1, p0, Lcom/zopim/android/sdk/model/ChatLog;->failed:Ljava/lang/Boolean;

    sget-object p1, Lcom/zopim/android/sdk/model/a;->a:[I

    invoke-virtual {p2}, Lcom/zopim/android/sdk/model/ChatLog$Type;->ordinal()I

    move-result p2

    aget p1, p1, p2

    packed-switch p1, :pswitch_data_0

    goto :goto_2

    :pswitch_0
    sget-object p1, Lcom/zopim/android/sdk/model/ChatLog$b;->e:Lcom/zopim/android/sdk/model/ChatLog$b;

    goto :goto_0

    :pswitch_1
    sget-object p1, Lcom/zopim/android/sdk/model/ChatLog$b;->c:Lcom/zopim/android/sdk/model/ChatLog$b;

    goto :goto_0

    :pswitch_2
    sget-object p1, Lcom/zopim/android/sdk/model/ChatLog$b;->b:Lcom/zopim/android/sdk/model/ChatLog$b;

    :goto_0
    invoke-virtual {p1}, Lcom/zopim/android/sdk/model/ChatLog$b;->a()Ljava/lang/String;

    move-result-object p1

    iput-object p1, p0, Lcom/zopim/android/sdk/model/ChatLog;->type:Ljava/lang/String;

    goto :goto_2

    :pswitch_3
    sget-object p1, Lcom/zopim/android/sdk/model/ChatLog$b;->a:Lcom/zopim/android/sdk/model/ChatLog$b;

    invoke-virtual {p1}, Lcom/zopim/android/sdk/model/ChatLog$b;->a()Ljava/lang/String;

    move-result-object p1

    iput-object p1, p0, Lcom/zopim/android/sdk/model/ChatLog;->type:Ljava/lang/String;

    sget-object p1, Lcom/zopim/android/sdk/model/ChatLog$a;->b:Lcom/zopim/android/sdk/model/ChatLog$a;

    goto :goto_1

    :pswitch_4
    sget-object p1, Lcom/zopim/android/sdk/model/ChatLog$b;->d:Lcom/zopim/android/sdk/model/ChatLog$b;

    invoke-virtual {p1}, Lcom/zopim/android/sdk/model/ChatLog$b;->a()Ljava/lang/String;

    move-result-object p1

    iput-object p1, p0, Lcom/zopim/android/sdk/model/ChatLog;->type:Ljava/lang/String;

    sget-object p1, Lcom/zopim/android/sdk/model/ChatLog$a;->a:Lcom/zopim/android/sdk/model/ChatLog$a;

    goto :goto_1

    :pswitch_5
    sget-object p1, Lcom/zopim/android/sdk/model/ChatLog$b;->a:Lcom/zopim/android/sdk/model/ChatLog$b;

    invoke-virtual {p1}, Lcom/zopim/android/sdk/model/ChatLog$b;->a()Ljava/lang/String;

    move-result-object p1

    iput-object p1, p0, Lcom/zopim/android/sdk/model/ChatLog;->type:Ljava/lang/String;

    sget-object p1, Lcom/zopim/android/sdk/model/ChatLog$a;->c:Lcom/zopim/android/sdk/model/ChatLog$a;

    goto :goto_1

    :pswitch_6
    sget-object p1, Lcom/zopim/android/sdk/model/ChatLog$b;->a:Lcom/zopim/android/sdk/model/ChatLog$b;

    invoke-virtual {p1}, Lcom/zopim/android/sdk/model/ChatLog$b;->a()Ljava/lang/String;

    move-result-object p1

    iput-object p1, p0, Lcom/zopim/android/sdk/model/ChatLog;->type:Ljava/lang/String;

    sget-object p1, Lcom/zopim/android/sdk/model/ChatLog$a;->d:Lcom/zopim/android/sdk/model/ChatLog$a;

    :goto_1
    invoke-virtual {p1}, Lcom/zopim/android/sdk/model/ChatLog$a;->a()Ljava/lang/String;

    move-result-object p1

    iput-object p1, p0, Lcom/zopim/android/sdk/model/ChatLog;->nick:Ljava/lang/String;

    :goto_2
    return-void

    :pswitch_data_0
    .packed-switch 0x1
        :pswitch_6
        :pswitch_5
        :pswitch_4
        :pswitch_3
        :pswitch_2
        :pswitch_1
        :pswitch_0
    .end packed-switch
.end method

.method static synthetic access$000()Ljava/lang/String;
    .locals 1

    sget-object v0, Lcom/zopim/android/sdk/model/ChatLog;->LOG_TAG:Ljava/lang/String;

    return-object v0
.end method

.method private setOptions(Ljava/lang/String;)V
    .locals 4
    .annotation runtime Lcom/fasterxml/jackson/annotation/JsonProperty;
        value = "options$string"
    .end annotation

    if-eqz p1, :cond_2

    invoke-virtual {p1}, Ljava/lang/String;->isEmpty()Z

    move-result v0

    if-eqz v0, :cond_0

    goto :goto_1

    :cond_0
    const-string v0, "/"

    invoke-virtual {p1, v0}, Ljava/lang/String;->split(Ljava/lang/String;)[Ljava/lang/String;

    move-result-object p1

    array-length v0, p1

    new-array v0, v0, [Lcom/zopim/android/sdk/model/ChatLog$Option;

    iput-object v0, p0, Lcom/zopim/android/sdk/model/ChatLog;->options:[Lcom/zopim/android/sdk/model/ChatLog$Option;

    const/4 v0, 0x0

    :goto_0
    array-length v1, p1

    if-ge v0, v1, :cond_1

    iget-object v1, p0, Lcom/zopim/android/sdk/model/ChatLog;->options:[Lcom/zopim/android/sdk/model/ChatLog$Option;

    new-instance v2, Lcom/zopim/android/sdk/model/ChatLog$Option;

    aget-object v3, p1, v0

    invoke-direct {v2, v3}, Lcom/zopim/android/sdk/model/ChatLog$Option;-><init>(Ljava/lang/String;)V

    aput-object v2, v1, v0

    add-int/lit8 v0, v0, 0x1

    goto :goto_0

    :cond_1
    return-void

    :cond_2
    :goto_1
    sget-object p1, Lcom/zopim/android/sdk/model/ChatLog;->LOG_TAG:Ljava/lang/String;

    const-string v0, "Can not set options with empty string"

    invoke-static {p1, v0}, Landroid/util/Log;->w(Ljava/lang/String;Ljava/lang/String;)I

    return-void
.end method

.method private setUnrated(Ljava/lang/String;)V
    .locals 2
    .annotation runtime Lcom/fasterxml/jackson/annotation/JsonProperty;
        value = "rating$string"
    .end annotation

    invoke-static {p1}, Lcom/zopim/android/sdk/model/ChatLog$Rating;->getRating(Ljava/lang/String;)Lcom/zopim/android/sdk/model/ChatLog$Rating;

    move-result-object v0

    sget-object v1, Lcom/zopim/android/sdk/model/ChatLog$Rating;->UNKNOWN:Lcom/zopim/android/sdk/model/ChatLog$Rating;

    if-ne v0, v1, :cond_0

    return-void

    :cond_0
    invoke-virtual {p0}, Lcom/zopim/android/sdk/model/ChatLog;->getRating()Lcom/zopim/android/sdk/model/ChatLog$Rating;

    move-result-object v0

    sget-object v1, Lcom/zopim/android/sdk/model/ChatLog$Rating;->UNKNOWN:Lcom/zopim/android/sdk/model/ChatLog$Rating;

    if-eq v0, v1, :cond_1

    invoke-static {p1}, Lcom/zopim/android/sdk/model/ChatLog$Rating;->getRating(Ljava/lang/String;)Lcom/zopim/android/sdk/model/ChatLog$Rating;

    move-result-object p1

    invoke-virtual {p0}, Lcom/zopim/android/sdk/model/ChatLog;->getRating()Lcom/zopim/android/sdk/model/ChatLog$Rating;

    move-result-object v0

    if-eq p1, v0, :cond_1

    return-void

    :cond_1
    sget-object p1, Lcom/zopim/android/sdk/model/ChatLog$Rating;->UNRATED:Lcom/zopim/android/sdk/model/ChatLog$Rating;

    invoke-virtual {p1}, Lcom/zopim/android/sdk/model/ChatLog$Rating;->getValue()Ljava/lang/String;

    move-result-object p1

    iput-object p1, p0, Lcom/zopim/android/sdk/model/ChatLog;->rating:Ljava/lang/String;

    return-void
.end method


# virtual methods
.method public compareTo(Lcom/zopim/android/sdk/model/ChatLog;)I
    .locals 3
    .param p1    # Lcom/zopim/android/sdk/model/ChatLog;
        .annotation build Landroidx/annotation/Nullable;
        .end annotation
    .end param

    const/4 v0, 0x0

    if-nez p1, :cond_0

    sget-object p1, Lcom/zopim/android/sdk/model/ChatLog;->LOG_TAG:Ljava/lang/String;

    const-string v1, "Passed parameter must not be null. Can not compare. Declaring them as same."

    invoke-static {p1, v1}, Landroid/util/Log;->w(Ljava/lang/String;Ljava/lang/String;)I

    return v0

    :cond_0
    iget-object v1, p0, Lcom/zopim/android/sdk/model/ChatLog;->timestamp:Ljava/lang/Long;

    if-eqz v1, :cond_2

    invoke-virtual {p1}, Lcom/zopim/android/sdk/model/ChatLog;->getTimestamp()Ljava/lang/Long;

    move-result-object v1

    if-nez v1, :cond_1

    goto :goto_0

    :cond_1
    :try_start_0
    iget-object v1, p0, Lcom/zopim/android/sdk/model/ChatLog;->timestamp:Ljava/lang/Long;

    invoke-virtual {p1}, Lcom/zopim/android/sdk/model/ChatLog;->getTimestamp()Ljava/lang/Long;

    move-result-object p1

    invoke-virtual {v1, p1}, Ljava/lang/Long;->compareTo(Ljava/lang/Long;)I

    move-result p1
    :try_end_0
    .catch Ljava/lang/NullPointerException; {:try_start_0 .. :try_end_0} :catch_0

    return p1

    :catch_0
    move-exception p1

    sget-object v1, Lcom/zopim/android/sdk/model/ChatLog;->LOG_TAG:Ljava/lang/String;

    const-string v2, "Error comparing chat logs. Timestamp was not initialized. Declaring them as same."

    invoke-static {v1, v2, p1}, Landroid/util/Log;->w(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)I

    return v0

    :cond_2
    :goto_0
    sget-object p1, Lcom/zopim/android/sdk/model/ChatLog;->LOG_TAG:Ljava/lang/String;

    const-string v1, "Error comparing chat logs. Timestamp was null. Declaring them as same."

    invoke-static {p1, v1}, Landroid/util/Log;->w(Ljava/lang/String;Ljava/lang/String;)I

    return v0
.end method

.method public bridge synthetic compareTo(Ljava/lang/Object;)I
    .locals 0
    .param p1    # Ljava/lang/Object;
        .annotation build Landroidx/annotation/Nullable;
        .end annotation
    .end param

    check-cast p1, Lcom/zopim/android/sdk/model/ChatLog;

    invoke-virtual {p0, p1}, Lcom/zopim/android/sdk/model/ChatLog;->compareTo(Lcom/zopim/android/sdk/model/ChatLog;)I

    move-result p1

    return p1
.end method

.method public getAttachment()Lcom/zopim/android/sdk/model/Attachment;
    .locals 1
    .annotation build Landroidx/annotation/Nullable;
    .end annotation

    iget-object v0, p0, Lcom/zopim/android/sdk/model/ChatLog;->attachment:Lcom/zopim/android/sdk/model/Attachment;

    return-object v0
.end method

.method public getComment()Ljava/lang/String;
    .locals 1

    iget-object v0, p0, Lcom/zopim/android/sdk/model/ChatLog;->comment:Ljava/lang/String;

    return-object v0
.end method

.method public getDisplayName()Ljava/lang/String;
    .locals 1
    .annotation build Landroidx/annotation/Nullable;
    .end annotation

    iget-object v0, p0, Lcom/zopim/android/sdk/model/ChatLog;->displayName:Ljava/lang/String;

    return-object v0
.end method

.method public getError()Lcom/zopim/android/sdk/model/ChatLog$Error;
    .locals 1
    .annotation build Landroidx/annotation/NonNull;
    .end annotation

    iget-object v0, p0, Lcom/zopim/android/sdk/model/ChatLog;->error:Ljava/lang/String;

    invoke-static {v0}, Lcom/zopim/android/sdk/model/ChatLog$Error;->getType(Ljava/lang/String;)Lcom/zopim/android/sdk/model/ChatLog$Error;

    move-result-object v0

    return-object v0
.end method

.method public getFile()Ljava/io/File;
    .locals 1
    .annotation build Landroidx/annotation/Nullable;
    .end annotation

    iget-object v0, p0, Lcom/zopim/android/sdk/model/ChatLog;->file:Ljava/io/File;

    return-object v0
.end method

.method public getFileName()Ljava/lang/String;
    .locals 1
    .annotation build Landroidx/annotation/Nullable;
    .end annotation

    iget-object v0, p0, Lcom/zopim/android/sdk/model/ChatLog;->fileName:Ljava/lang/String;

    return-object v0
.end method

.method public getMessage()Ljava/lang/String;
    .locals 1
    .annotation build Landroidx/annotation/Nullable;
    .end annotation

    iget-object v0, p0, Lcom/zopim/android/sdk/model/ChatLog;->message:Ljava/lang/String;

    return-object v0
.end method

.method public getNick()Ljava/lang/String;
    .locals 1
    .annotation build Landroidx/annotation/Nullable;
    .end annotation

    iget-object v0, p0, Lcom/zopim/android/sdk/model/ChatLog;->nick:Ljava/lang/String;

    return-object v0
.end method

.method public getOptions()[Lcom/zopim/android/sdk/model/ChatLog$Option;
    .locals 1
    .annotation build Landroidx/annotation/NonNull;
    .end annotation

    iget-object v0, p0, Lcom/zopim/android/sdk/model/ChatLog;->options:[Lcom/zopim/android/sdk/model/ChatLog$Option;

    if-nez v0, :cond_0

    const/4 v0, 0x0

    new-array v0, v0, [Lcom/zopim/android/sdk/model/ChatLog$Option;

    return-object v0

    :cond_0
    iget-object v0, p0, Lcom/zopim/android/sdk/model/ChatLog;->options:[Lcom/zopim/android/sdk/model/ChatLog$Option;

    return-object v0
.end method

.method public getProgress()I
    .locals 1
    .annotation build Landroidx/annotation/NonNull;
    .end annotation

    iget v0, p0, Lcom/zopim/android/sdk/model/ChatLog;->uploadProgress:I

    return v0
.end method

.method public getRating()Lcom/zopim/android/sdk/model/ChatLog$Rating;
    .locals 1

    iget-object v0, p0, Lcom/zopim/android/sdk/model/ChatLog;->rating:Ljava/lang/String;

    invoke-static {v0}, Lcom/zopim/android/sdk/model/ChatLog$Rating;->getRating(Ljava/lang/String;)Lcom/zopim/android/sdk/model/ChatLog$Rating;

    move-result-object v0

    return-object v0
.end method

.method public getTimestamp()Ljava/lang/Long;
    .locals 1
    .annotation build Landroidx/annotation/Nullable;
    .end annotation

    iget-object v0, p0, Lcom/zopim/android/sdk/model/ChatLog;->timestamp:Ljava/lang/Long;

    return-object v0
.end method

.method public getType()Lcom/zopim/android/sdk/model/ChatLog$Type;
    .locals 2
    .annotation build Landroidx/annotation/NonNull;
    .end annotation

    sget-object v0, Lcom/zopim/android/sdk/model/a;->c:[I

    iget-object v1, p0, Lcom/zopim/android/sdk/model/ChatLog;->type:Ljava/lang/String;

    invoke-static {v1}, Lcom/zopim/android/sdk/model/ChatLog$b;->a(Ljava/lang/String;)Lcom/zopim/android/sdk/model/ChatLog$b;

    move-result-object v1

    invoke-virtual {v1}, Lcom/zopim/android/sdk/model/ChatLog$b;->ordinal()I

    move-result v1

    aget v0, v0, v1

    packed-switch v0, :pswitch_data_0

    sget-object v0, Lcom/zopim/android/sdk/model/ChatLog$Type;->UNKNOWN:Lcom/zopim/android/sdk/model/ChatLog$Type;

    return-object v0

    :pswitch_0
    sget-object v0, Lcom/zopim/android/sdk/model/ChatLog$Type;->CHAT_RATING:Lcom/zopim/android/sdk/model/ChatLog$Type;

    return-object v0

    :pswitch_1
    sget-object v0, Lcom/zopim/android/sdk/model/ChatLog$Type;->ATTACHMENT_UPLOAD:Lcom/zopim/android/sdk/model/ChatLog$Type;

    return-object v0

    :pswitch_2
    sget-object v0, Lcom/zopim/android/sdk/model/ChatLog$Type;->SYSTEM_OFFLINE:Lcom/zopim/android/sdk/model/ChatLog$Type;

    return-object v0

    :pswitch_3
    sget-object v0, Lcom/zopim/android/sdk/model/ChatLog$Type;->MEMBER_LEAVE:Lcom/zopim/android/sdk/model/ChatLog$Type;

    return-object v0

    :pswitch_4
    sget-object v0, Lcom/zopim/android/sdk/model/ChatLog$Type;->MEMBER_JOIN:Lcom/zopim/android/sdk/model/ChatLog$Type;

    return-object v0

    :pswitch_5
    sget-object v0, Lcom/zopim/android/sdk/model/ChatLog$Type;->CHAT_MSG_SYSTEM:Lcom/zopim/android/sdk/model/ChatLog$Type;

    return-object v0

    :pswitch_6
    sget-object v0, Lcom/zopim/android/sdk/model/a;->b:[I

    iget-object v1, p0, Lcom/zopim/android/sdk/model/ChatLog;->nick:Ljava/lang/String;

    invoke-static {v1}, Lcom/zopim/android/sdk/model/ChatLog$a;->a(Ljava/lang/String;)Lcom/zopim/android/sdk/model/ChatLog$a;

    move-result-object v1

    invoke-virtual {v1}, Lcom/zopim/android/sdk/model/ChatLog$a;->ordinal()I

    move-result v1

    aget v0, v0, v1

    packed-switch v0, :pswitch_data_1

    sget-object v0, Lcom/zopim/android/sdk/model/ChatLog$Type;->UNKNOWN:Lcom/zopim/android/sdk/model/ChatLog$Type;

    return-object v0

    :pswitch_7
    sget-object v0, Lcom/zopim/android/sdk/model/ChatLog$Type;->CHAT_MSG_VISITOR:Lcom/zopim/android/sdk/model/ChatLog$Type;

    return-object v0

    :pswitch_8
    sget-object v0, Lcom/zopim/android/sdk/model/ChatLog$Type;->CHAT_MSG_AGENT:Lcom/zopim/android/sdk/model/ChatLog$Type;

    return-object v0

    :pswitch_9
    sget-object v0, Lcom/zopim/android/sdk/model/ChatLog$Type;->CHAT_MSG_TRIGGER:Lcom/zopim/android/sdk/model/ChatLog$Type;

    return-object v0

    :pswitch_a
    sget-object v0, Lcom/zopim/android/sdk/model/ChatLog$Type;->CHAT_MSG_SYSTEM:Lcom/zopim/android/sdk/model/ChatLog$Type;

    return-object v0

    :pswitch_data_0
    .packed-switch 0x1
        :pswitch_6
        :pswitch_5
        :pswitch_4
        :pswitch_3
        :pswitch_2
        :pswitch_1
        :pswitch_0
        :pswitch_0
        :pswitch_0
    .end packed-switch

    :pswitch_data_1
    .packed-switch 0x1
        :pswitch_a
        :pswitch_9
        :pswitch_8
        :pswitch_7
    .end packed-switch
.end method

.method public getUploadUrl()Ljava/net/URL;
    .locals 4
    .annotation build Landroidx/annotation/Nullable;
    .end annotation

    iget-object v0, p0, Lcom/zopim/android/sdk/model/ChatLog;->uploadUrl:Ljava/lang/String;

    const/4 v1, 0x0

    if-eqz v0, :cond_0

    :try_start_0
    new-instance v0, Ljava/net/URL;

    iget-object v2, p0, Lcom/zopim/android/sdk/model/ChatLog;->uploadUrl:Ljava/lang/String;

    invoke-direct {v0, v2}, Ljava/net/URL;-><init>(Ljava/lang/String;)V
    :try_end_0
    .catch Ljava/net/MalformedURLException; {:try_start_0 .. :try_end_0} :catch_0

    return-object v0

    :catch_0
    move-exception v0

    sget-object v2, Lcom/zopim/android/sdk/model/ChatLog;->LOG_TAG:Ljava/lang/String;

    const-string v3, "Can not retrieve url. "

    invoke-static {v2, v3, v0}, Landroid/util/Log;->w(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)I

    :cond_0
    return-object v1
.end method

.method public getVisitorQueue()Ljava/lang/Integer;
    .locals 1
    .annotation build Landroidx/annotation/Nullable;
    .end annotation

    iget-object v0, p0, Lcom/zopim/android/sdk/model/ChatLog;->visitorQueue:Ljava/lang/Integer;

    return-object v0
.end method

.method public isFailed()Ljava/lang/Boolean;
    .locals 1
    .annotation build Landroidx/annotation/Nullable;
    .end annotation

    iget-object v0, p0, Lcom/zopim/android/sdk/model/ChatLog;->failed:Ljava/lang/Boolean;

    return-object v0
.end method

.method public isUnverified()Ljava/lang/Boolean;
    .locals 1
    .annotation build Landroidx/annotation/Nullable;
    .end annotation

    iget-object v0, p0, Lcom/zopim/android/sdk/model/ChatLog;->unverified:Ljava/lang/Boolean;

    return-object v0
.end method

.method public setComment(Ljava/lang/String;)V
    .locals 0

    iput-object p1, p0, Lcom/zopim/android/sdk/model/ChatLog;->comment:Ljava/lang/String;

    return-void
.end method

.method public setError(Lcom/zopim/android/sdk/model/ChatLog$Error;)V
    .locals 0

    invoke-virtual {p1}, Lcom/zopim/android/sdk/model/ChatLog$Error;->getValue()Ljava/lang/String;

    move-result-object p1

    iput-object p1, p0, Lcom/zopim/android/sdk/model/ChatLog;->error:Ljava/lang/String;

    return-void
.end method

.method public setFailed(Z)V
    .locals 0

    invoke-static {p1}, Ljava/lang/Boolean;->valueOf(Z)Ljava/lang/Boolean;

    move-result-object p1

    iput-object p1, p0, Lcom/zopim/android/sdk/model/ChatLog;->failed:Ljava/lang/Boolean;

    return-void
.end method

.method public setFile(Ljava/io/File;)V
    .locals 0

    iput-object p1, p0, Lcom/zopim/android/sdk/model/ChatLog;->file:Ljava/io/File;

    return-void
.end method

.method public setProgress(I)V
    .locals 1

    if-ltz p1, :cond_2

    const/16 v0, 0x64

    if-le p1, v0, :cond_0

    goto :goto_1

    :cond_0
    iget v0, p0, Lcom/zopim/android/sdk/model/ChatLog;->uploadProgress:I

    if-ge p1, v0, :cond_1

    sget-object p1, Lcom/zopim/android/sdk/model/ChatLog;->LOG_TAG:Ljava/lang/String;

    const-string v0, "Supplied progress must not be less then current progress. Progress will not be updated."

    :goto_0
    invoke-static {p1, v0}, Landroid/util/Log;->i(Ljava/lang/String;Ljava/lang/String;)I

    return-void

    :cond_1
    iput p1, p0, Lcom/zopim/android/sdk/model/ChatLog;->uploadProgress:I

    return-void

    :cond_2
    :goto_1
    sget-object p1, Lcom/zopim/android/sdk/model/ChatLog;->LOG_TAG:Ljava/lang/String;

    const-string v0, "Supplied progress must be in range 0 - 100. Progress will not be updated."

    goto :goto_0
.end method

.method public setRating(Lcom/zopim/android/sdk/model/ChatLog$Rating;)V
    .locals 0

    invoke-virtual {p1}, Lcom/zopim/android/sdk/model/ChatLog$Rating;->getValue()Ljava/lang/String;

    move-result-object p1

    iput-object p1, p0, Lcom/zopim/android/sdk/model/ChatLog;->rating:Ljava/lang/String;

    return-void
.end method

.method public toString()Ljava/lang/String;
    .locals 2

    new-instance v0, Ljava/lang/StringBuilder;

    invoke-direct {v0}, Ljava/lang/StringBuilder;-><init>()V

    const-string v1, "type:"

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v1, p0, Lcom/zopim/android/sdk/model/ChatLog;->type:Ljava/lang/String;

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, ", name:"

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v1, p0, Lcom/zopim/android/sdk/model/ChatLog;->displayName:Ljava/lang/String;

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, ", msg:"

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v1, p0, Lcom/zopim/android/sdk/model/ChatLog;->message:Ljava/lang/String;

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, ", time:"

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v1, p0, Lcom/zopim/android/sdk/model/ChatLog;->timestamp:Ljava/lang/Long;

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/Object;)Ljava/lang/StringBuilder;

    const-string v1, ", url:"

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v1, p0, Lcom/zopim/android/sdk/model/ChatLog;->uploadUrl:Ljava/lang/String;

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v0

    return-object v0
.end method
