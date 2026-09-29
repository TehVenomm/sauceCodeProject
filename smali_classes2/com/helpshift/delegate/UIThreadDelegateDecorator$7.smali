.class Lcom/helpshift/delegate/UIThreadDelegateDecorator$7;
.super Lcom/helpshift/common/domain/F;
.source "UIThreadDelegateDecorator.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lcom/helpshift/delegate/UIThreadDelegateDecorator;->displayAttachmentFile(Ljava/io/File;)V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lcom/helpshift/delegate/UIThreadDelegateDecorator;

.field final synthetic val$attachmentFile:Ljava/io/File;


# direct methods
.method constructor <init>(Lcom/helpshift/delegate/UIThreadDelegateDecorator;Ljava/io/File;)V
    .locals 0

    .line 90
    iput-object p1, p0, Lcom/helpshift/delegate/UIThreadDelegateDecorator$7;->this$0:Lcom/helpshift/delegate/UIThreadDelegateDecorator;

    iput-object p2, p0, Lcom/helpshift/delegate/UIThreadDelegateDecorator$7;->val$attachmentFile:Ljava/io/File;

    invoke-direct {p0}, Lcom/helpshift/common/domain/F;-><init>()V

    return-void
.end method


# virtual methods
.method public f()V
    .locals 2

    .line 93
    iget-object v0, p0, Lcom/helpshift/delegate/UIThreadDelegateDecorator$7;->this$0:Lcom/helpshift/delegate/UIThreadDelegateDecorator;

    invoke-static {v0}, Lcom/helpshift/delegate/UIThreadDelegateDecorator;->access$000(Lcom/helpshift/delegate/UIThreadDelegateDecorator;)Lcom/helpshift/delegate/RootDelegate;

    move-result-object v0

    iget-object v1, p0, Lcom/helpshift/delegate/UIThreadDelegateDecorator$7;->val$attachmentFile:Ljava/io/File;

    invoke-interface {v0, v1}, Lcom/helpshift/delegate/RootDelegate;->displayAttachmentFile(Ljava/io/File;)V

    return-void
.end method
