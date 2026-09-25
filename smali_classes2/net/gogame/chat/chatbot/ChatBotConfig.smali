.class public Lnet/gogame/chat/chatbot/ChatBotConfig;
.super Ljava/lang/Object;
.source "ChatBotConfig.java"

# interfaces
.implements Ljava/io/Serializable;


# instance fields
.field private appId:Ljava/lang/String;

.field private guid:Ljava/lang/String;


# direct methods
.method public constructor <init>()V
    .locals 0

    .line 5
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public getAppId()Ljava/lang/String;
    .locals 1

    .line 11
    iget-object v0, p0, Lnet/gogame/chat/chatbot/ChatBotConfig;->appId:Ljava/lang/String;

    return-object v0
.end method

.method public getGuid()Ljava/lang/String;
    .locals 1

    .line 19
    iget-object v0, p0, Lnet/gogame/chat/chatbot/ChatBotConfig;->guid:Ljava/lang/String;

    return-object v0
.end method

.method public setAppId(Ljava/lang/String;)V
    .locals 0

    .line 15
    iput-object p1, p0, Lnet/gogame/chat/chatbot/ChatBotConfig;->appId:Ljava/lang/String;

    return-void
.end method

.method public setGuid(Ljava/lang/String;)V
    .locals 0

    .line 23
    iput-object p1, p0, Lnet/gogame/chat/chatbot/ChatBotConfig;->guid:Ljava/lang/String;

    return-void
.end method
