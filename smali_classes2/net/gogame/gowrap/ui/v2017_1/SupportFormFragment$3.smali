.class Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment$3;
.super Ljava/lang/Object;
.source "SupportFormFragment.java"

# interfaces
.implements Landroid/text/TextWatcher;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment;->onCreateView(Landroid/view/LayoutInflater;Landroid/view/ViewGroup;Landroid/os/Bundle;)Landroid/view/View;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment;

.field final synthetic val$sendButton:Landroid/view/View;

.field final synthetic val$supportRequestCollector:Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment$SupportRequestCollector;


# direct methods
.method constructor <init>(Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment;Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment$SupportRequestCollector;Landroid/view/View;)V
    .locals 0

    .line 143
    iput-object p1, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment$3;->this$0:Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment;

    iput-object p2, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment$3;->val$supportRequestCollector:Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment$SupportRequestCollector;

    iput-object p3, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment$3;->val$sendButton:Landroid/view/View;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public afterTextChanged(Landroid/text/Editable;)V
    .locals 1

    .line 157
    iget-object p1, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment$3;->val$supportRequestCollector:Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment$SupportRequestCollector;

    invoke-interface {p1}, Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment$SupportRequestCollector;->collect()Lnet/gogame/gowrap/support/SupportRequest;

    move-result-object p1

    .line 158
    iget-object v0, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment$3;->val$sendButton:Landroid/view/View;

    invoke-static {p1}, Lnet/gogame/gowrap/support/SupportManager;->isValid(Lnet/gogame/gowrap/support/SupportRequest;)Z

    move-result p1

    invoke-virtual {v0, p1}, Landroid/view/View;->setSelected(Z)V

    return-void
.end method

.method public beforeTextChanged(Ljava/lang/CharSequence;III)V
    .locals 0

    return-void
.end method

.method public onTextChanged(Ljava/lang/CharSequence;III)V
    .locals 0

    return-void
.end method
