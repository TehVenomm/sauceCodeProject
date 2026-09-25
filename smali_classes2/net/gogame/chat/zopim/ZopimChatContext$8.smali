.class synthetic Lnet/gogame/chat/zopim/ZopimChatContext$8;
.super Ljava/lang/Object;
.source "ZopimChatContext.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lnet/gogame/chat/zopim/ZopimChatContext;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x1008
    name = null
.end annotation


# static fields
.field static final synthetic $SwitchMap$com$zopim$android$sdk$model$ChatLog$Rating:[I

.field static final synthetic $SwitchMap$com$zopim$android$sdk$model$ChatLog$Type:[I

.field static final synthetic $SwitchMap$net$gogame$chat$ChatContext$Rating:[I


# direct methods
.method static constructor <clinit>()V
    .locals 5

    .line 345
    invoke-static {}, Lnet/gogame/chat/ChatContext$Rating;->values()[Lnet/gogame/chat/ChatContext$Rating;

    move-result-object v0

    array-length v0, v0

    new-array v0, v0, [I

    sput-object v0, Lnet/gogame/chat/zopim/ZopimChatContext$8;->$SwitchMap$net$gogame$chat$ChatContext$Rating:[I

    const/4 v0, 0x1

    :try_start_0
    sget-object v1, Lnet/gogame/chat/zopim/ZopimChatContext$8;->$SwitchMap$net$gogame$chat$ChatContext$Rating:[I

    sget-object v2, Lnet/gogame/chat/ChatContext$Rating;->GOOD:Lnet/gogame/chat/ChatContext$Rating;

    invoke-virtual {v2}, Lnet/gogame/chat/ChatContext$Rating;->ordinal()I

    move-result v2

    aput v0, v1, v2
    :try_end_0
    .catch Ljava/lang/NoSuchFieldError; {:try_start_0 .. :try_end_0} :catch_0

    :catch_0
    const/4 v1, 0x2

    :try_start_1
    sget-object v2, Lnet/gogame/chat/zopim/ZopimChatContext$8;->$SwitchMap$net$gogame$chat$ChatContext$Rating:[I

    sget-object v3, Lnet/gogame/chat/ChatContext$Rating;->BAD:Lnet/gogame/chat/ChatContext$Rating;

    invoke-virtual {v3}, Lnet/gogame/chat/ChatContext$Rating;->ordinal()I

    move-result v3

    aput v1, v2, v3
    :try_end_1
    .catch Ljava/lang/NoSuchFieldError; {:try_start_1 .. :try_end_1} :catch_1

    :catch_1
    const/4 v2, 0x3

    :try_start_2
    sget-object v3, Lnet/gogame/chat/zopim/ZopimChatContext$8;->$SwitchMap$net$gogame$chat$ChatContext$Rating:[I

    sget-object v4, Lnet/gogame/chat/ChatContext$Rating;->UNRATED:Lnet/gogame/chat/ChatContext$Rating;

    invoke-virtual {v4}, Lnet/gogame/chat/ChatContext$Rating;->ordinal()I

    move-result v4

    aput v2, v3, v4
    :try_end_2
    .catch Ljava/lang/NoSuchFieldError; {:try_start_2 .. :try_end_2} :catch_2

    .line 329
    :catch_2
    invoke-static {}, Lcom/zopim/android/sdk/model/ChatLog$Rating;->values()[Lcom/zopim/android/sdk/model/ChatLog$Rating;

    move-result-object v3

    array-length v3, v3

    new-array v3, v3, [I

    sput-object v3, Lnet/gogame/chat/zopim/ZopimChatContext$8;->$SwitchMap$com$zopim$android$sdk$model$ChatLog$Rating:[I

    :try_start_3
    sget-object v3, Lnet/gogame/chat/zopim/ZopimChatContext$8;->$SwitchMap$com$zopim$android$sdk$model$ChatLog$Rating:[I

    sget-object v4, Lcom/zopim/android/sdk/model/ChatLog$Rating;->GOOD:Lcom/zopim/android/sdk/model/ChatLog$Rating;

    invoke-virtual {v4}, Lcom/zopim/android/sdk/model/ChatLog$Rating;->ordinal()I

    move-result v4

    aput v0, v3, v4
    :try_end_3
    .catch Ljava/lang/NoSuchFieldError; {:try_start_3 .. :try_end_3} :catch_3

    :catch_3
    :try_start_4
    sget-object v3, Lnet/gogame/chat/zopim/ZopimChatContext$8;->$SwitchMap$com$zopim$android$sdk$model$ChatLog$Rating:[I

    sget-object v4, Lcom/zopim/android/sdk/model/ChatLog$Rating;->BAD:Lcom/zopim/android/sdk/model/ChatLog$Rating;

    invoke-virtual {v4}, Lcom/zopim/android/sdk/model/ChatLog$Rating;->ordinal()I

    move-result v4

    aput v1, v3, v4
    :try_end_4
    .catch Ljava/lang/NoSuchFieldError; {:try_start_4 .. :try_end_4} :catch_4

    :catch_4
    :try_start_5
    sget-object v3, Lnet/gogame/chat/zopim/ZopimChatContext$8;->$SwitchMap$com$zopim$android$sdk$model$ChatLog$Rating:[I

    sget-object v4, Lcom/zopim/android/sdk/model/ChatLog$Rating;->UNRATED:Lcom/zopim/android/sdk/model/ChatLog$Rating;

    invoke-virtual {v4}, Lcom/zopim/android/sdk/model/ChatLog$Rating;->ordinal()I

    move-result v4

    aput v2, v3, v4
    :try_end_5
    .catch Ljava/lang/NoSuchFieldError; {:try_start_5 .. :try_end_5} :catch_5

    .line 220
    :catch_5
    invoke-static {}, Lcom/zopim/android/sdk/model/ChatLog$Type;->values()[Lcom/zopim/android/sdk/model/ChatLog$Type;

    move-result-object v3

    array-length v3, v3

    new-array v3, v3, [I

    sput-object v3, Lnet/gogame/chat/zopim/ZopimChatContext$8;->$SwitchMap$com$zopim$android$sdk$model$ChatLog$Type:[I

    :try_start_6
    sget-object v3, Lnet/gogame/chat/zopim/ZopimChatContext$8;->$SwitchMap$com$zopim$android$sdk$model$ChatLog$Type:[I

    sget-object v4, Lcom/zopim/android/sdk/model/ChatLog$Type;->CHAT_MSG_AGENT:Lcom/zopim/android/sdk/model/ChatLog$Type;

    invoke-virtual {v4}, Lcom/zopim/android/sdk/model/ChatLog$Type;->ordinal()I

    move-result v4

    aput v0, v3, v4
    :try_end_6
    .catch Ljava/lang/NoSuchFieldError; {:try_start_6 .. :try_end_6} :catch_6

    :catch_6
    :try_start_7
    sget-object v0, Lnet/gogame/chat/zopim/ZopimChatContext$8;->$SwitchMap$com$zopim$android$sdk$model$ChatLog$Type:[I

    sget-object v3, Lcom/zopim/android/sdk/model/ChatLog$Type;->CHAT_MSG_VISITOR:Lcom/zopim/android/sdk/model/ChatLog$Type;

    invoke-virtual {v3}, Lcom/zopim/android/sdk/model/ChatLog$Type;->ordinal()I

    move-result v3

    aput v1, v0, v3
    :try_end_7
    .catch Ljava/lang/NoSuchFieldError; {:try_start_7 .. :try_end_7} :catch_7

    :catch_7
    :try_start_8
    sget-object v0, Lnet/gogame/chat/zopim/ZopimChatContext$8;->$SwitchMap$com$zopim$android$sdk$model$ChatLog$Type:[I

    sget-object v1, Lcom/zopim/android/sdk/model/ChatLog$Type;->CHAT_MSG_TRIGGER:Lcom/zopim/android/sdk/model/ChatLog$Type;

    invoke-virtual {v1}, Lcom/zopim/android/sdk/model/ChatLog$Type;->ordinal()I

    move-result v1

    aput v2, v0, v1
    :try_end_8
    .catch Ljava/lang/NoSuchFieldError; {:try_start_8 .. :try_end_8} :catch_8

    :catch_8
    :try_start_9
    sget-object v0, Lnet/gogame/chat/zopim/ZopimChatContext$8;->$SwitchMap$com$zopim$android$sdk$model$ChatLog$Type:[I

    sget-object v1, Lcom/zopim/android/sdk/model/ChatLog$Type;->CHAT_MSG_SYSTEM:Lcom/zopim/android/sdk/model/ChatLog$Type;

    invoke-virtual {v1}, Lcom/zopim/android/sdk/model/ChatLog$Type;->ordinal()I

    move-result v1

    const/4 v2, 0x4

    aput v2, v0, v1
    :try_end_9
    .catch Ljava/lang/NoSuchFieldError; {:try_start_9 .. :try_end_9} :catch_9

    :catch_9
    :try_start_a
    sget-object v0, Lnet/gogame/chat/zopim/ZopimChatContext$8;->$SwitchMap$com$zopim$android$sdk$model$ChatLog$Type:[I

    sget-object v1, Lcom/zopim/android/sdk/model/ChatLog$Type;->MEMBER_JOIN:Lcom/zopim/android/sdk/model/ChatLog$Type;

    invoke-virtual {v1}, Lcom/zopim/android/sdk/model/ChatLog$Type;->ordinal()I

    move-result v1

    const/4 v2, 0x5

    aput v2, v0, v1
    :try_end_a
    .catch Ljava/lang/NoSuchFieldError; {:try_start_a .. :try_end_a} :catch_a

    :catch_a
    :try_start_b
    sget-object v0, Lnet/gogame/chat/zopim/ZopimChatContext$8;->$SwitchMap$com$zopim$android$sdk$model$ChatLog$Type:[I

    sget-object v1, Lcom/zopim/android/sdk/model/ChatLog$Type;->MEMBER_LEAVE:Lcom/zopim/android/sdk/model/ChatLog$Type;

    invoke-virtual {v1}, Lcom/zopim/android/sdk/model/ChatLog$Type;->ordinal()I

    move-result v1

    const/4 v2, 0x6

    aput v2, v0, v1
    :try_end_b
    .catch Ljava/lang/NoSuchFieldError; {:try_start_b .. :try_end_b} :catch_b

    :catch_b
    :try_start_c
    sget-object v0, Lnet/gogame/chat/zopim/ZopimChatContext$8;->$SwitchMap$com$zopim$android$sdk$model$ChatLog$Type:[I

    sget-object v1, Lcom/zopim/android/sdk/model/ChatLog$Type;->SYSTEM_OFFLINE:Lcom/zopim/android/sdk/model/ChatLog$Type;

    invoke-virtual {v1}, Lcom/zopim/android/sdk/model/ChatLog$Type;->ordinal()I

    move-result v1

    const/4 v2, 0x7

    aput v2, v0, v1
    :try_end_c
    .catch Ljava/lang/NoSuchFieldError; {:try_start_c .. :try_end_c} :catch_c

    :catch_c
    :try_start_d
    sget-object v0, Lnet/gogame/chat/zopim/ZopimChatContext$8;->$SwitchMap$com$zopim$android$sdk$model$ChatLog$Type:[I

    sget-object v1, Lcom/zopim/android/sdk/model/ChatLog$Type;->ATTACHMENT_UPLOAD:Lcom/zopim/android/sdk/model/ChatLog$Type;

    invoke-virtual {v1}, Lcom/zopim/android/sdk/model/ChatLog$Type;->ordinal()I

    move-result v1

    const/16 v2, 0x8

    aput v2, v0, v1
    :try_end_d
    .catch Ljava/lang/NoSuchFieldError; {:try_start_d .. :try_end_d} :catch_d

    :catch_d
    :try_start_e
    sget-object v0, Lnet/gogame/chat/zopim/ZopimChatContext$8;->$SwitchMap$com$zopim$android$sdk$model$ChatLog$Type:[I

    sget-object v1, Lcom/zopim/android/sdk/model/ChatLog$Type;->CHAT_RATING:Lcom/zopim/android/sdk/model/ChatLog$Type;

    invoke-virtual {v1}, Lcom/zopim/android/sdk/model/ChatLog$Type;->ordinal()I

    move-result v1

    const/16 v2, 0x9

    aput v2, v0, v1
    :try_end_e
    .catch Ljava/lang/NoSuchFieldError; {:try_start_e .. :try_end_e} :catch_e

    :catch_e
    :try_start_f
    sget-object v0, Lnet/gogame/chat/zopim/ZopimChatContext$8;->$SwitchMap$com$zopim$android$sdk$model$ChatLog$Type:[I

    sget-object v1, Lcom/zopim/android/sdk/model/ChatLog$Type;->UNKNOWN:Lcom/zopim/android/sdk/model/ChatLog$Type;

    invoke-virtual {v1}, Lcom/zopim/android/sdk/model/ChatLog$Type;->ordinal()I

    move-result v1

    const/16 v2, 0xa

    aput v2, v0, v1
    :try_end_f
    .catch Ljava/lang/NoSuchFieldError; {:try_start_f .. :try_end_f} :catch_f

    :catch_f
    return-void
.end method
