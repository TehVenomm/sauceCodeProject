.class Lcom/helpshift/conversation/domainmodel/ConversationController$CreateConversationStateHolder;
.super Ljava/lang/Object;
.source "ConversationController.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lcom/helpshift/conversation/domainmodel/ConversationController;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x2
    name = "CreateConversationStateHolder"
.end annotation


# instance fields
.field final description:Ljava/lang/String;

.field final imagePickerFile:Lcom/helpshift/conversation/dto/ImagePickerFile;

.field private final startNewConversationInternalF:Lcom/helpshift/common/domain/F;

.field final synthetic this$0:Lcom/helpshift/conversation/domainmodel/ConversationController;

.field final userProvidedEmail:Ljava/lang/String;

.field final userProvidedName:Ljava/lang/String;


# direct methods
.method constructor <init>(Lcom/helpshift/conversation/domainmodel/ConversationController;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Lcom/helpshift/conversation/dto/ImagePickerFile;)V
    .locals 1

    .line 2114
    iput-object p1, p0, Lcom/helpshift/conversation/domainmodel/ConversationController$CreateConversationStateHolder;->this$0:Lcom/helpshift/conversation/domainmodel/ConversationController;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 2106
    new-instance p1, Lcom/helpshift/common/domain/One;

    new-instance v0, Lcom/helpshift/conversation/domainmodel/ConversationController$CreateConversationStateHolder$1;

    invoke-direct {v0, p0}, Lcom/helpshift/conversation/domainmodel/ConversationController$CreateConversationStateHolder$1;-><init>(Lcom/helpshift/conversation/domainmodel/ConversationController$CreateConversationStateHolder;)V

    invoke-direct {p1, v0}, Lcom/helpshift/common/domain/One;-><init>(Lcom/helpshift/common/domain/F;)V

    iput-object p1, p0, Lcom/helpshift/conversation/domainmodel/ConversationController$CreateConversationStateHolder;->startNewConversationInternalF:Lcom/helpshift/common/domain/F;

    .line 2115
    iput-object p2, p0, Lcom/helpshift/conversation/domainmodel/ConversationController$CreateConversationStateHolder;->description:Ljava/lang/String;

    .line 2116
    iput-object p3, p0, Lcom/helpshift/conversation/domainmodel/ConversationController$CreateConversationStateHolder;->userProvidedName:Ljava/lang/String;

    .line 2117
    iput-object p4, p0, Lcom/helpshift/conversation/domainmodel/ConversationController$CreateConversationStateHolder;->userProvidedEmail:Ljava/lang/String;

    .line 2118
    iput-object p5, p0, Lcom/helpshift/conversation/domainmodel/ConversationController$CreateConversationStateHolder;->imagePickerFile:Lcom/helpshift/conversation/dto/ImagePickerFile;

    return-void
.end method


# virtual methods
.method getStartNewConversationInternalF()Lcom/helpshift/common/domain/F;
    .locals 1

    .line 2122
    iget-object v0, p0, Lcom/helpshift/conversation/domainmodel/ConversationController$CreateConversationStateHolder;->startNewConversationInternalF:Lcom/helpshift/common/domain/F;

    return-object v0
.end method
