.class Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment$5;
.super Ljava/lang/Object;
.source "SupportFormFragment.java"

# interfaces
.implements Landroid/view/View$OnClickListener;


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

.field final synthetic val$dialog:Landroid/app/Dialog;


# direct methods
.method constructor <init>(Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment;Landroid/app/Dialog;)V
    .locals 0

    .line 197
    iput-object p1, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment$5;->this$0:Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment;

    iput-object p2, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment$5;->val$dialog:Landroid/app/Dialog;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public onClick(Landroid/view/View;)V
    .locals 0

    .line 201
    iget-object p1, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment$5;->val$dialog:Landroid/app/Dialog;

    invoke-virtual {p1}, Landroid/app/Dialog;->show()V

    return-void
.end method
