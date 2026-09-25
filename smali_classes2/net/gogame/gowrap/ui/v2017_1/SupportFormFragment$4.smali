.class Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment$4;
.super Ljava/lang/Object;
.source "SupportFormFragment.java"

# interfaces
.implements Landroid/widget/AdapterView$OnItemClickListener;


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

.field final synthetic val$categoryAdapter:Landroid/widget/ArrayAdapter;

.field final synthetic val$categoryField:Landroid/widget/TextView;

.field final synthetic val$dialog:Landroid/app/Dialog;

.field final synthetic val$listView:Landroid/widget/ListView;

.field final synthetic val$sendButton:Landroid/view/View;

.field final synthetic val$supportRequestCollector:Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment$SupportRequestCollector;


# direct methods
.method constructor <init>(Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment;Landroid/widget/ListView;Landroid/widget/ArrayAdapter;Landroid/widget/TextView;Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment$SupportRequestCollector;Landroid/view/View;Landroid/app/Dialog;)V
    .locals 0

    .line 175
    iput-object p1, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment$4;->this$0:Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment;

    iput-object p2, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment$4;->val$listView:Landroid/widget/ListView;

    iput-object p3, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment$4;->val$categoryAdapter:Landroid/widget/ArrayAdapter;

    iput-object p4, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment$4;->val$categoryField:Landroid/widget/TextView;

    iput-object p5, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment$4;->val$supportRequestCollector:Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment$SupportRequestCollector;

    iput-object p6, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment$4;->val$sendButton:Landroid/view/View;

    iput-object p7, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment$4;->val$dialog:Landroid/app/Dialog;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public onItemClick(Landroid/widget/AdapterView;Landroid/view/View;IJ)V
    .locals 0
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Landroid/widget/AdapterView<",
            "*>;",
            "Landroid/view/View;",
            "IJ)V"
        }
    .end annotation

    .line 179
    iget-object p1, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment$4;->val$listView:Landroid/widget/ListView;

    const/4 p2, 0x1

    invoke-virtual {p1, p3, p2}, Landroid/widget/ListView;->setItemChecked(IZ)V

    .line 180
    iget-object p1, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment$4;->val$categoryAdapter:Landroid/widget/ArrayAdapter;

    .line 181
    invoke-virtual {p1, p3}, Landroid/widget/ArrayAdapter;->getItem(I)Ljava/lang/Object;

    move-result-object p1

    check-cast p1, Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment$SupportCategoryWrapper;

    if-eqz p1, :cond_0

    .line 183
    iget-object p2, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment$4;->this$0:Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment;

    invoke-virtual {p1}, Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment$SupportCategoryWrapper;->getSupportCategory()Lnet/gogame/gowrap/support/SupportCategory;

    move-result-object p1

    invoke-static {p2, p1}, Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment;->access$102(Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment;Lnet/gogame/gowrap/support/SupportCategory;)Lnet/gogame/gowrap/support/SupportCategory;

    .line 185
    :cond_0
    iget-object p1, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment$4;->this$0:Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment;

    invoke-static {p1}, Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment;->access$100(Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment;)Lnet/gogame/gowrap/support/SupportCategory;

    move-result-object p1

    if-eqz p1, :cond_1

    .line 186
    iget-object p1, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment$4;->val$categoryField:Landroid/widget/TextView;

    iget-object p2, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment$4;->this$0:Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment;

    invoke-static {p2}, Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment;->access$100(Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment;)Lnet/gogame/gowrap/support/SupportCategory;

    move-result-object p2

    invoke-virtual {p2}, Lnet/gogame/gowrap/support/SupportCategory;->getStringResourceId()I

    move-result p2

    invoke-virtual {p1, p2}, Landroid/widget/TextView;->setText(I)V

    goto :goto_0

    .line 188
    :cond_1
    iget-object p1, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment$4;->val$categoryField:Landroid/widget/TextView;

    const/4 p2, 0x0

    invoke-virtual {p1, p2}, Landroid/widget/TextView;->setText(Ljava/lang/CharSequence;)V

    .line 191
    :goto_0
    iget-object p1, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment$4;->val$supportRequestCollector:Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment$SupportRequestCollector;

    invoke-interface {p1}, Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment$SupportRequestCollector;->collect()Lnet/gogame/gowrap/support/SupportRequest;

    move-result-object p1

    .line 192
    iget-object p2, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment$4;->val$sendButton:Landroid/view/View;

    invoke-static {p1}, Lnet/gogame/gowrap/support/SupportManager;->isValid(Lnet/gogame/gowrap/support/SupportRequest;)Z

    move-result p1

    invoke-virtual {p2, p1}, Landroid/view/View;->setSelected(Z)V

    .line 193
    iget-object p1, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment$4;->val$dialog:Landroid/app/Dialog;

    invoke-virtual {p1}, Landroid/app/Dialog;->dismiss()V

    return-void
.end method
