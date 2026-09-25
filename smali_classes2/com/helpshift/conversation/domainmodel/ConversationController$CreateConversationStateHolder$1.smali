.class Lcom/helpshift/conversation/domainmodel/ConversationController$CreateConversationStateHolder$1;
.super Lcom/helpshift/common/domain/F;
.source "ConversationController.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lcom/helpshift/conversation/domainmodel/ConversationController$CreateConversationStateHolder;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$1:Lcom/helpshift/conversation/domainmodel/ConversationController$CreateConversationStateHolder;


# direct methods
.method constructor <init>(Lcom/helpshift/conversation/domainmodel/ConversationController$CreateConversationStateHolder;)V
    .locals 0

    .line 2106
    iput-object p1, p0, Lcom/helpshift/conversation/domainmodel/ConversationController$CreateConversationStateHolder$1;->this$1:Lcom/helpshift/conversation/domainmodel/ConversationController$CreateConversationStateHolder;

    invoke-direct {p0}, Lcom/helpshift/common/domain/F;-><init>()V

    return-void
.end method


# virtual methods
.method public f()V
    .locals 5

    .line 2109
    iget-object v0, p0, Lcom/helpshift/conversation/domainmodel/ConversationController$CreateConversationStateHolder$1;->this$1:Lcom/helpshift/conversation/domainmodel/ConversationController$CreateConversationStateHolder;

    iget-object v0, v0, Lcom/helpshift/conversation/domainmodel/ConversationController$CreateConversationStateHolder;->this$0:Lcom/helpshift/conversation/domainmodel/ConversationController;

    iget-object v1, p0, Lcom/helpshift/conversation/domainmodel/ConversationController$CreateConversationStateHolder$1;->this$1:Lcom/helpshift/conversation/domainmodel/ConversationController$CreateConversationStateHolder;

    iget-object v1, v1, Lcom/helpshift/conversation/domainmodel/ConversationController$CreateConversationStateHolder;->description:Ljava/lang/String;

    iget-object v2, p0, Lcom/helpshift/conversation/domainmodel/ConversationController$CreateConversationStateHolder$1;->this$1:Lcom/helpshift/conversation/domainmodel/ConversationController$CreateConversationStateHolder;

    iget-object v2, v2, Lcom/helpshift/conversation/domainmodel/ConversationController$CreateConversationStateHolder;->userProvidedName:Ljava/lang/String;

    iget-object v3, p0, Lcom/helpshift/conversation/domainmodel/ConversationController$CreateConversationStateHolder$1;->this$1:Lcom/helpshift/conversation/domainmodel/ConversationController$CreateConversationStateHolder;

    iget-object v3, v3, Lcom/helpshift/conversation/domainmodel/ConversationController$CreateConversationStateHolder;->userProvidedEmail:Ljava/lang/String;

    iget-object v4, p0, Lcom/helpshift/conversation/domainmodel/ConversationController$CreateConversationStateHolder$1;->this$1:Lcom/helpshift/conversation/domainmodel/ConversationController$CreateConversationStateHolder;

    iget-object v4, v4, Lcom/helpshift/conversation/domainmodel/ConversationController$CreateConversationStateHolder;->imagePickerFile:Lcom/helpshift/conversation/dto/ImagePickerFile;

    invoke-virtual {v0, v1, v2, v3, v4}, Lcom/helpshift/conversation/domainmodel/ConversationController;->startNewConversationInternal(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Lcom/helpshift/conversation/dto/ImagePickerFile;)V

    return-void
.end method
