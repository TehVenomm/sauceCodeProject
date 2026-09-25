.class Lnet/gogame/gowrap/ui/v2017_1/SupportFragment$9;
.super Ljava/lang/Object;
.source "SupportFragment.java"

# interfaces
.implements Landroid/widget/ExpandableListView$OnChildClickListener;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lnet/gogame/gowrap/ui/v2017_1/SupportFragment;->onCreateView(Landroid/view/LayoutInflater;Landroid/view/ViewGroup;Landroid/os/Bundle;)Landroid/view/View;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lnet/gogame/gowrap/ui/v2017_1/SupportFragment;

.field final synthetic val$htmlTemplate:Ljava/lang/String;

.field final synthetic val$uiContext:Lnet/gogame/gowrap/ui/UIContext;


# direct methods
.method constructor <init>(Lnet/gogame/gowrap/ui/v2017_1/SupportFragment;Lnet/gogame/gowrap/ui/UIContext;Ljava/lang/String;)V
    .locals 0

    .line 284
    iput-object p1, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFragment$9;->this$0:Lnet/gogame/gowrap/ui/v2017_1/SupportFragment;

    iput-object p2, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFragment$9;->val$uiContext:Lnet/gogame/gowrap/ui/UIContext;

    iput-object p3, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFragment$9;->val$htmlTemplate:Ljava/lang/String;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public onChildClick(Landroid/widget/ExpandableListView;Landroid/view/View;IIJ)Z
    .locals 1

    .line 289
    iget-object p1, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFragment$9;->this$0:Lnet/gogame/gowrap/ui/v2017_1/SupportFragment;

    invoke-static {p1}, Lnet/gogame/gowrap/ui/v2017_1/SupportFragment;->access$800(Lnet/gogame/gowrap/ui/v2017_1/SupportFragment;)Lnet/gogame/gowrap/ui/v2017_1/FaqExpandableListAdapter;

    move-result-object p1

    invoke-virtual {p1, p3, p4}, Lnet/gogame/gowrap/ui/v2017_1/FaqExpandableListAdapter;->getChild(II)Ljava/lang/Object;

    move-result-object p1

    check-cast p1, Lnet/gogame/gowrap/model/faq/Article;

    const/4 p2, 0x1

    if-eqz p1, :cond_0

    .line 291
    iget-object p3, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFragment$9;->val$uiContext:Lnet/gogame/gowrap/ui/UIContext;

    if-eqz p3, :cond_0

    .line 292
    iget-object p3, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFragment$9;->val$htmlTemplate:Ljava/lang/String;

    if-eqz p3, :cond_0

    .line 293
    invoke-static {}, Ljava/util/Locale;->getDefault()Ljava/util/Locale;

    move-result-object p3

    iget-object p4, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFragment$9;->val$htmlTemplate:Ljava/lang/String;

    const/4 p5, 0x2

    new-array p5, p5, [Ljava/lang/Object;

    const/4 p6, 0x0

    .line 294
    invoke-virtual {p1}, Lnet/gogame/gowrap/model/faq/Article;->getTitle()Ljava/lang/String;

    move-result-object v0

    invoke-static {v0}, Lnet/gogame/gowrap/support/StringUtils;->escapeHtml(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v0

    aput-object v0, p5, p6

    invoke-virtual {p1}, Lnet/gogame/gowrap/model/faq/Article;->getBody()Ljava/lang/String;

    move-result-object p1

    aput-object p1, p5, p2

    .line 293
    invoke-static {p3, p4, p5}, Ljava/lang/String;->format(Ljava/util/Locale;Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object p1

    .line 295
    iget-object p3, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFragment$9;->val$uiContext:Lnet/gogame/gowrap/ui/UIContext;

    const/4 p4, 0x0

    invoke-interface {p3, p1, p4}, Lnet/gogame/gowrap/ui/UIContext;->loadHtml(Ljava/lang/String;Ljava/lang/String;)V

    :cond_0
    return p2
.end method
