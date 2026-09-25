.class Lcom/helpshift/support/HSReviewFragment$3;
.super Ljava/lang/Object;
.source "HSReviewFragment.java"

# interfaces
.implements Landroid/content/DialogInterface$OnClickListener;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lcom/helpshift/support/HSReviewFragment;->initAlertDialog(Landroidx/fragment/app/FragmentActivity;)Landroid/app/Dialog;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lcom/helpshift/support/HSReviewFragment;


# direct methods
.method constructor <init>(Lcom/helpshift/support/HSReviewFragment;)V
    .locals 0

    .line 138
    iput-object p1, p0, Lcom/helpshift/support/HSReviewFragment$3;->this$0:Lcom/helpshift/support/HSReviewFragment;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public onClick(Landroid/content/DialogInterface;I)V
    .locals 0

    .line 141
    iget-object p1, p0, Lcom/helpshift/support/HSReviewFragment$3;->this$0:Lcom/helpshift/support/HSReviewFragment;

    const-string p2, "later"

    invoke-virtual {p1, p2}, Lcom/helpshift/support/HSReviewFragment;->sendReviewActionEvent(Ljava/lang/String;)V

    .line 142
    iget-object p1, p0, Lcom/helpshift/support/HSReviewFragment$3;->this$0:Lcom/helpshift/support/HSReviewFragment;

    const/4 p2, 0x2

    invoke-virtual {p1, p2}, Lcom/helpshift/support/HSReviewFragment;->sendAlertToRateAppAction(I)V

    return-void
.end method
