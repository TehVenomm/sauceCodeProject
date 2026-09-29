.class Lcom/helpshift/views/bottomsheet/HSBottomSheet$1;
.super Ljava/lang/Object;
.source "HSBottomSheet.java"

# interfaces
.implements Ljava/lang/Runnable;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lcom/helpshift/views/bottomsheet/HSBottomSheet;->initiateReferenceViewAttachment()V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lcom/helpshift/views/bottomsheet/HSBottomSheet;


# direct methods
.method constructor <init>(Lcom/helpshift/views/bottomsheet/HSBottomSheet;)V
    .locals 0

    .line 122
    iput-object p1, p0, Lcom/helpshift/views/bottomsheet/HSBottomSheet$1;->this$0:Lcom/helpshift/views/bottomsheet/HSBottomSheet;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public run()V
    .locals 1

    .line 125
    iget-object v0, p0, Lcom/helpshift/views/bottomsheet/HSBottomSheet$1;->this$0:Lcom/helpshift/views/bottomsheet/HSBottomSheet;

    invoke-virtual {v0}, Lcom/helpshift/views/bottomsheet/HSBottomSheet;->showOnReferenceView()V

    return-void
.end method
