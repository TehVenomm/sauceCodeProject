.class synthetic Lcom/helpshift/support/conversations/messages/AdminAttachmentMessageDataBinder$2;
.super Ljava/lang/Object;
.source "AdminAttachmentMessageDataBinder.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lcom/helpshift/support/conversations/messages/AdminAttachmentMessageDataBinder;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x1008
    name = null
.end annotation


# static fields
.field static final synthetic $SwitchMap$com$helpshift$conversation$activeconversation$message$AdminAttachmentMessageDM$AdminGenericAttachmentState:[I


# direct methods
.method static constructor <clinit>()V
    .locals 3

    .line 39
    invoke-static {}, Lcom/helpshift/conversation/activeconversation/message/AdminAttachmentMessageDM$AdminGenericAttachmentState;->values()[Lcom/helpshift/conversation/activeconversation/message/AdminAttachmentMessageDM$AdminGenericAttachmentState;

    move-result-object v0

    array-length v0, v0

    new-array v0, v0, [I

    sput-object v0, Lcom/helpshift/support/conversations/messages/AdminAttachmentMessageDataBinder$2;->$SwitchMap$com$helpshift$conversation$activeconversation$message$AdminAttachmentMessageDM$AdminGenericAttachmentState:[I

    :try_start_0
    sget-object v0, Lcom/helpshift/support/conversations/messages/AdminAttachmentMessageDataBinder$2;->$SwitchMap$com$helpshift$conversation$activeconversation$message$AdminAttachmentMessageDM$AdminGenericAttachmentState:[I

    sget-object v1, Lcom/helpshift/conversation/activeconversation/message/AdminAttachmentMessageDM$AdminGenericAttachmentState;->DOWNLOAD_NOT_STARTED:Lcom/helpshift/conversation/activeconversation/message/AdminAttachmentMessageDM$AdminGenericAttachmentState;

    invoke-virtual {v1}, Lcom/helpshift/conversation/activeconversation/message/AdminAttachmentMessageDM$AdminGenericAttachmentState;->ordinal()I

    move-result v1

    const/4 v2, 0x1

    aput v2, v0, v1
    :try_end_0
    .catch Ljava/lang/NoSuchFieldError; {:try_start_0 .. :try_end_0} :catch_0

    :catch_0
    :try_start_1
    sget-object v0, Lcom/helpshift/support/conversations/messages/AdminAttachmentMessageDataBinder$2;->$SwitchMap$com$helpshift$conversation$activeconversation$message$AdminAttachmentMessageDM$AdminGenericAttachmentState:[I

    sget-object v1, Lcom/helpshift/conversation/activeconversation/message/AdminAttachmentMessageDM$AdminGenericAttachmentState;->DOWNLOADING:Lcom/helpshift/conversation/activeconversation/message/AdminAttachmentMessageDM$AdminGenericAttachmentState;

    invoke-virtual {v1}, Lcom/helpshift/conversation/activeconversation/message/AdminAttachmentMessageDM$AdminGenericAttachmentState;->ordinal()I

    move-result v1

    const/4 v2, 0x2

    aput v2, v0, v1
    :try_end_1
    .catch Ljava/lang/NoSuchFieldError; {:try_start_1 .. :try_end_1} :catch_1

    :catch_1
    :try_start_2
    sget-object v0, Lcom/helpshift/support/conversations/messages/AdminAttachmentMessageDataBinder$2;->$SwitchMap$com$helpshift$conversation$activeconversation$message$AdminAttachmentMessageDM$AdminGenericAttachmentState:[I

    sget-object v1, Lcom/helpshift/conversation/activeconversation/message/AdminAttachmentMessageDM$AdminGenericAttachmentState;->DOWNLOADED:Lcom/helpshift/conversation/activeconversation/message/AdminAttachmentMessageDM$AdminGenericAttachmentState;

    invoke-virtual {v1}, Lcom/helpshift/conversation/activeconversation/message/AdminAttachmentMessageDM$AdminGenericAttachmentState;->ordinal()I

    move-result v1

    const/4 v2, 0x3

    aput v2, v0, v1
    :try_end_2
    .catch Ljava/lang/NoSuchFieldError; {:try_start_2 .. :try_end_2} :catch_2

    :catch_2
    return-void
.end method
