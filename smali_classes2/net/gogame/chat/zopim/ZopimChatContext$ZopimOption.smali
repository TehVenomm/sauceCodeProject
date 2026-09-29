.class public Lnet/gogame/chat/zopim/ZopimChatContext$ZopimOption;
.super Ljava/lang/Object;
.source "ZopimChatContext.java"

# interfaces
.implements Lnet/gogame/chat/ChatAdapterViewFactory$Option;


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lnet/gogame/chat/zopim/ZopimChatContext;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x9
    name = "ZopimOption"
.end annotation


# instance fields
.field private final option:Lcom/zopim/android/sdk/model/ChatLog$Option;


# direct methods
.method public constructor <init>(Lcom/zopim/android/sdk/model/ChatLog$Option;)V
    .locals 0

    .line 384
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 386
    iput-object p1, p0, Lnet/gogame/chat/zopim/ZopimChatContext$ZopimOption;->option:Lcom/zopim/android/sdk/model/ChatLog$Option;

    return-void
.end method


# virtual methods
.method public getLabel()Ljava/lang/String;
    .locals 1

    .line 391
    iget-object v0, p0, Lnet/gogame/chat/zopim/ZopimChatContext$ZopimOption;->option:Lcom/zopim/android/sdk/model/ChatLog$Option;

    invoke-virtual {v0}, Lcom/zopim/android/sdk/model/ChatLog$Option;->getLabel()Ljava/lang/String;

    move-result-object v0

    return-object v0
.end method

.method public isSelected()Z
    .locals 1

    .line 396
    iget-object v0, p0, Lnet/gogame/chat/zopim/ZopimChatContext$ZopimOption;->option:Lcom/zopim/android/sdk/model/ChatLog$Option;

    invoke-virtual {v0}, Lcom/zopim/android/sdk/model/ChatLog$Option;->isSelected()Z

    move-result v0

    return v0
.end method
