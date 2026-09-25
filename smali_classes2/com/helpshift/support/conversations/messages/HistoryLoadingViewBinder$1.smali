.class synthetic Lcom/helpshift/support/conversations/messages/HistoryLoadingViewBinder$1;
.super Ljava/lang/Object;
.source "HistoryLoadingViewBinder.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lcom/helpshift/support/conversations/messages/HistoryLoadingViewBinder;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x1008
    name = null
.end annotation


# static fields
.field static final synthetic $SwitchMap$com$helpshift$conversation$activeconversation$message$HistoryLoadingState:[I


# direct methods
.method static constructor <clinit>()V
    .locals 3

    .line 33
    invoke-static {}, Lcom/helpshift/conversation/activeconversation/message/HistoryLoadingState;->values()[Lcom/helpshift/conversation/activeconversation/message/HistoryLoadingState;

    move-result-object v0

    array-length v0, v0

    new-array v0, v0, [I

    sput-object v0, Lcom/helpshift/support/conversations/messages/HistoryLoadingViewBinder$1;->$SwitchMap$com$helpshift$conversation$activeconversation$message$HistoryLoadingState:[I

    :try_start_0
    sget-object v0, Lcom/helpshift/support/conversations/messages/HistoryLoadingViewBinder$1;->$SwitchMap$com$helpshift$conversation$activeconversation$message$HistoryLoadingState:[I

    sget-object v1, Lcom/helpshift/conversation/activeconversation/message/HistoryLoadingState;->NONE:Lcom/helpshift/conversation/activeconversation/message/HistoryLoadingState;

    invoke-virtual {v1}, Lcom/helpshift/conversation/activeconversation/message/HistoryLoadingState;->ordinal()I

    move-result v1

    const/4 v2, 0x1

    aput v2, v0, v1
    :try_end_0
    .catch Ljava/lang/NoSuchFieldError; {:try_start_0 .. :try_end_0} :catch_0

    :catch_0
    :try_start_1
    sget-object v0, Lcom/helpshift/support/conversations/messages/HistoryLoadingViewBinder$1;->$SwitchMap$com$helpshift$conversation$activeconversation$message$HistoryLoadingState:[I

    sget-object v1, Lcom/helpshift/conversation/activeconversation/message/HistoryLoadingState;->LOADING:Lcom/helpshift/conversation/activeconversation/message/HistoryLoadingState;

    invoke-virtual {v1}, Lcom/helpshift/conversation/activeconversation/message/HistoryLoadingState;->ordinal()I

    move-result v1

    const/4 v2, 0x2

    aput v2, v0, v1
    :try_end_1
    .catch Ljava/lang/NoSuchFieldError; {:try_start_1 .. :try_end_1} :catch_1

    :catch_1
    :try_start_2
    sget-object v0, Lcom/helpshift/support/conversations/messages/HistoryLoadingViewBinder$1;->$SwitchMap$com$helpshift$conversation$activeconversation$message$HistoryLoadingState:[I

    sget-object v1, Lcom/helpshift/conversation/activeconversation/message/HistoryLoadingState;->ERROR:Lcom/helpshift/conversation/activeconversation/message/HistoryLoadingState;

    invoke-virtual {v1}, Lcom/helpshift/conversation/activeconversation/message/HistoryLoadingState;->ordinal()I

    move-result v1

    const/4 v2, 0x3

    aput v2, v0, v1
    :try_end_2
    .catch Ljava/lang/NoSuchFieldError; {:try_start_2 .. :try_end_2} :catch_2

    :catch_2
    return-void
.end method
