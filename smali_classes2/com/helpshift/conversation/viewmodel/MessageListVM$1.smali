.class Lcom/helpshift/conversation/viewmodel/MessageListVM$1;
.super Ljava/lang/Object;
.source "MessageListVM.java"

# interfaces
.implements Ljava/util/Comparator;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lcom/helpshift/conversation/viewmodel/MessageListVM;->getSortMessagesComparator()Ljava/util/Comparator;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation

.annotation system Ldalvik/annotation/Signature;
    value = {
        "Ljava/lang/Object;",
        "Ljava/util/Comparator<",
        "Lcom/helpshift/conversation/activeconversation/message/MessageDM;",
        ">;"
    }
.end annotation


# instance fields
.field final synthetic this$0:Lcom/helpshift/conversation/viewmodel/MessageListVM;


# direct methods
.method constructor <init>(Lcom/helpshift/conversation/viewmodel/MessageListVM;)V
    .locals 0

    .line 437
    iput-object p1, p0, Lcom/helpshift/conversation/viewmodel/MessageListVM$1;->this$0:Lcom/helpshift/conversation/viewmodel/MessageListVM;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public compare(Lcom/helpshift/conversation/activeconversation/message/MessageDM;Lcom/helpshift/conversation/activeconversation/message/MessageDM;)I
    .locals 6

    .line 440
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/MessageListVM$1;->this$0:Lcom/helpshift/conversation/viewmodel/MessageListVM;

    iget-object v1, p1, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->conversationLocalId:Ljava/lang/Long;

    invoke-virtual {v1}, Ljava/lang/Long;->longValue()J

    move-result-wide v1

    invoke-virtual {v0, v1, v2}, Lcom/helpshift/conversation/viewmodel/MessageListVM;->getUIConversation(J)Lcom/helpshift/conversation/activeconversation/UIConversation;

    move-result-object v0

    .line 441
    iget-object v1, p0, Lcom/helpshift/conversation/viewmodel/MessageListVM$1;->this$0:Lcom/helpshift/conversation/viewmodel/MessageListVM;

    iget-object v2, p2, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->conversationLocalId:Ljava/lang/Long;

    invoke-virtual {v2}, Ljava/lang/Long;->longValue()J

    move-result-wide v2

    invoke-virtual {v1, v2, v3}, Lcom/helpshift/conversation/viewmodel/MessageListVM;->getUIConversation(J)Lcom/helpshift/conversation/activeconversation/UIConversation;

    move-result-object v1

    const/4 v2, 0x0

    if-eqz v0, :cond_5

    if-nez v1, :cond_0

    goto :goto_0

    .line 446
    :cond_0
    iget v0, v0, Lcom/helpshift/conversation/activeconversation/UIConversation;->index:I

    invoke-static {v0}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v0

    .line 447
    iget v1, v1, Lcom/helpshift/conversation/activeconversation/UIConversation;->index:I

    invoke-static {v1}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v1

    .line 449
    invoke-virtual {v0}, Ljava/lang/Integer;->intValue()I

    move-result v3

    invoke-virtual {v1}, Ljava/lang/Integer;->intValue()I

    move-result v4

    const/4 v5, -0x1

    if-ge v3, v4, :cond_1

    return v5

    .line 453
    :cond_1
    invoke-virtual {v0}, Ljava/lang/Integer;->intValue()I

    move-result v0

    invoke-virtual {v1}, Ljava/lang/Integer;->intValue()I

    move-result v1

    const/4 v3, 0x1

    if-le v0, v1, :cond_2

    return v3

    .line 460
    :cond_2
    invoke-virtual {p1}, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->getEpochCreatedAtTime()J

    move-result-wide v0

    .line 461
    invoke-virtual {p2}, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->getEpochCreatedAtTime()J

    move-result-wide p1

    cmp-long v4, v0, p1

    if-lez v4, :cond_3

    return v3

    :cond_3
    cmp-long v3, v0, p1

    if-gez v3, :cond_4

    return v5

    :cond_4
    return v2

    :cond_5
    :goto_0
    return v2
.end method

.method public bridge synthetic compare(Ljava/lang/Object;Ljava/lang/Object;)I
    .locals 0

    .line 437
    check-cast p1, Lcom/helpshift/conversation/activeconversation/message/MessageDM;

    check-cast p2, Lcom/helpshift/conversation/activeconversation/message/MessageDM;

    invoke-virtual {p0, p1, p2}, Lcom/helpshift/conversation/viewmodel/MessageListVM$1;->compare(Lcom/helpshift/conversation/activeconversation/message/MessageDM;Lcom/helpshift/conversation/activeconversation/message/MessageDM;)I

    move-result p1

    return p1
.end method
