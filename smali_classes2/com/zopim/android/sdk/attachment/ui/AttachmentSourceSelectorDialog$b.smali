.class Lcom/zopim/android/sdk/attachment/ui/AttachmentSourceSelectorDialog$b;
.super Landroid/widget/ArrayAdapter;


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lcom/zopim/android/sdk/attachment/ui/AttachmentSourceSelectorDialog;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = "b"
.end annotation

.annotation system Ldalvik/annotation/Signature;
    value = {
        "Landroid/widget/ArrayAdapter<",
        "Lcom/zopim/android/sdk/attachment/ui/AttachmentSourceSelectorDialog$c;",
        ">;"
    }
.end annotation


# instance fields
.field final synthetic a:Lcom/zopim/android/sdk/attachment/ui/AttachmentSourceSelectorDialog;

.field private b:Landroid/content/Context;


# direct methods
.method constructor <init>(Lcom/zopim/android/sdk/attachment/ui/AttachmentSourceSelectorDialog;Landroid/content/Context;ILjava/util/List;)V
    .locals 0
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Landroid/content/Context;",
            "I",
            "Ljava/util/List<",
            "Lcom/zopim/android/sdk/attachment/ui/AttachmentSourceSelectorDialog$c;",
            ">;)V"
        }
    .end annotation

    iput-object p1, p0, Lcom/zopim/android/sdk/attachment/ui/AttachmentSourceSelectorDialog$b;->a:Lcom/zopim/android/sdk/attachment/ui/AttachmentSourceSelectorDialog;

    invoke-direct {p0, p2, p3, p4}, Landroid/widget/ArrayAdapter;-><init>(Landroid/content/Context;ILjava/util/List;)V

    iput-object p2, p0, Lcom/zopim/android/sdk/attachment/ui/AttachmentSourceSelectorDialog$b;->b:Landroid/content/Context;

    return-void
.end method


# virtual methods
.method public getView(ILandroid/view/View;Landroid/view/ViewGroup;)Landroid/view/View;
    .locals 2

    invoke-virtual {p0, p1}, Lcom/zopim/android/sdk/attachment/ui/AttachmentSourceSelectorDialog$b;->getItem(I)Ljava/lang/Object;

    move-result-object p1

    check-cast p1, Lcom/zopim/android/sdk/attachment/ui/AttachmentSourceSelectorDialog$c;

    if-nez p2, :cond_0

    iget-object p2, p0, Lcom/zopim/android/sdk/attachment/ui/AttachmentSourceSelectorDialog$b;->b:Landroid/content/Context;

    invoke-static {p2}, Landroid/view/LayoutInflater;->from(Landroid/content/Context;)Landroid/view/LayoutInflater;

    move-result-object p2

    sget v0, Lcom/zopim/android/sdk/R$layout;->row_attachment_source_selector:I

    const/4 v1, 0x0

    invoke-virtual {p2, v0, p3, v1}, Landroid/view/LayoutInflater;->inflate(ILandroid/view/ViewGroup;Z)Landroid/view/View;

    move-result-object p2

    :cond_0
    sget p3, Lcom/zopim/android/sdk/R$id;->attachment_selector_image:I

    invoke-virtual {p2, p3}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object p3

    check-cast p3, Landroid/widget/ImageView;

    iget-object v0, p0, Lcom/zopim/android/sdk/attachment/ui/AttachmentSourceSelectorDialog$b;->b:Landroid/content/Context;

    invoke-virtual {p1}, Lcom/zopim/android/sdk/attachment/ui/AttachmentSourceSelectorDialog$c;->a()I

    move-result v1

    invoke-static {v0, v1}, Landroidx/core/content/ContextCompat;->getDrawable(Landroid/content/Context;I)Landroid/graphics/drawable/Drawable;

    move-result-object v0

    invoke-virtual {p3, v0}, Landroid/widget/ImageView;->setImageDrawable(Landroid/graphics/drawable/Drawable;)V

    sget p3, Lcom/zopim/android/sdk/R$id;->attachment_selector_text:I

    invoke-virtual {p2, p3}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object p3

    check-cast p3, Landroid/widget/TextView;

    invoke-virtual {p1}, Lcom/zopim/android/sdk/attachment/ui/AttachmentSourceSelectorDialog$c;->b()Ljava/lang/String;

    move-result-object p1

    invoke-virtual {p3, p1}, Landroid/widget/TextView;->setText(Ljava/lang/CharSequence;)V

    return-object p2
.end method
