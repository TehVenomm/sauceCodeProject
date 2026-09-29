.class final Lcom/helpshift/conversation/ConversationUtil$2;
.super Ljava/lang/Object;
.source "ConversationUtil.java"

# interfaces
.implements Ljava/util/Comparator;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lcom/helpshift/conversation/ConversationUtil;->sortMessagesBasedOnCreatedAt(Ljava/util/List;)V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x8
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


# direct methods
.method constructor <init>()V
    .locals 0

    .line 62
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public compare(Lcom/helpshift/conversation/activeconversation/message/MessageDM;Lcom/helpshift/conversation/activeconversation/message/MessageDM;)I
    .locals 2

    .line 65
    invoke-virtual {p1}, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->getEpochCreatedAtTime()J

    move-result-wide v0

    .line 66
    invoke-virtual {p2}, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->getEpochCreatedAtTime()J

    move-result-wide p1

    .line 67
    invoke-static {v0, v1, p1, p2}, Lcom/helpshift/conversation/ConversationUtil;->access$000(JJ)I

    move-result p1

    return p1
.end method

.method public bridge synthetic compare(Ljava/lang/Object;Ljava/lang/Object;)I
    .locals 0

    .line 62
    check-cast p1, Lcom/helpshift/conversation/activeconversation/message/MessageDM;

    check-cast p2, Lcom/helpshift/conversation/activeconversation/message/MessageDM;

    invoke-virtual {p0, p1, p2}, Lcom/helpshift/conversation/ConversationUtil$2;->compare(Lcom/helpshift/conversation/activeconversation/message/MessageDM;Lcom/helpshift/conversation/activeconversation/message/MessageDM;)I

    move-result p1

    return p1
.end method
