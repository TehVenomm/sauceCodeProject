.class synthetic Lcom/helpshift/conversation/activeconversation/message/UserResponseMessageForOptionInput$1;
.super Ljava/lang/Object;
.source "UserResponseMessageForOptionInput.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lcom/helpshift/conversation/activeconversation/message/UserResponseMessageForOptionInput;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x1008
    name = null
.end annotation


# static fields
.field static final synthetic $SwitchMap$com$helpshift$conversation$activeconversation$message$MessageType:[I


# direct methods
.method static constructor <clinit>()V
    .locals 3

    .line 82
    invoke-static {}, Lcom/helpshift/conversation/activeconversation/message/MessageType;->values()[Lcom/helpshift/conversation/activeconversation/message/MessageType;

    move-result-object v0

    array-length v0, v0

    new-array v0, v0, [I

    sput-object v0, Lcom/helpshift/conversation/activeconversation/message/UserResponseMessageForOptionInput$1;->$SwitchMap$com$helpshift$conversation$activeconversation$message$MessageType:[I

    :try_start_0
    sget-object v0, Lcom/helpshift/conversation/activeconversation/message/UserResponseMessageForOptionInput$1;->$SwitchMap$com$helpshift$conversation$activeconversation$message$MessageType:[I

    sget-object v1, Lcom/helpshift/conversation/activeconversation/message/MessageType;->ADMIN_TEXT_WITH_OPTION_INPUT:Lcom/helpshift/conversation/activeconversation/message/MessageType;

    invoke-virtual {v1}, Lcom/helpshift/conversation/activeconversation/message/MessageType;->ordinal()I

    move-result v1

    const/4 v2, 0x1

    aput v2, v0, v1
    :try_end_0
    .catch Ljava/lang/NoSuchFieldError; {:try_start_0 .. :try_end_0} :catch_0

    :catch_0
    :try_start_1
    sget-object v0, Lcom/helpshift/conversation/activeconversation/message/UserResponseMessageForOptionInput$1;->$SwitchMap$com$helpshift$conversation$activeconversation$message$MessageType:[I

    sget-object v1, Lcom/helpshift/conversation/activeconversation/message/MessageType;->FAQ_LIST_WITH_OPTION_INPUT:Lcom/helpshift/conversation/activeconversation/message/MessageType;

    invoke-virtual {v1}, Lcom/helpshift/conversation/activeconversation/message/MessageType;->ordinal()I

    move-result v1

    const/4 v2, 0x2

    aput v2, v0, v1
    :try_end_1
    .catch Ljava/lang/NoSuchFieldError; {:try_start_1 .. :try_end_1} :catch_1

    :catch_1
    return-void
.end method
