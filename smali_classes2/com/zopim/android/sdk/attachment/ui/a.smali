.class Lcom/zopim/android/sdk/attachment/ui/a;
.super Ljava/lang/Object;

# interfaces
.implements Landroid/widget/AdapterView$OnItemClickListener;


# instance fields
.field final synthetic a:Landroidx/fragment/app/Fragment;

.field final synthetic b:Lcom/zopim/android/sdk/attachment/ui/AttachmentSourceSelectorDialog;


# direct methods
.method constructor <init>(Lcom/zopim/android/sdk/attachment/ui/AttachmentSourceSelectorDialog;Landroidx/fragment/app/Fragment;)V
    .locals 0

    iput-object p1, p0, Lcom/zopim/android/sdk/attachment/ui/a;->b:Lcom/zopim/android/sdk/attachment/ui/AttachmentSourceSelectorDialog;

    iput-object p2, p0, Lcom/zopim/android/sdk/attachment/ui/a;->a:Landroidx/fragment/app/Fragment;

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

    invoke-virtual {p1}, Landroid/widget/AdapterView;->getAdapter()Landroid/widget/Adapter;

    move-result-object p1

    invoke-interface {p1, p3}, Landroid/widget/Adapter;->getItem(I)Ljava/lang/Object;

    move-result-object p1

    check-cast p1, Lcom/zopim/android/sdk/attachment/ui/AttachmentSourceSelectorDialog$c;

    sget-object p2, Lcom/zopim/android/sdk/attachment/ui/b;->a:[I

    invoke-virtual {p1}, Lcom/zopim/android/sdk/attachment/ui/AttachmentSourceSelectorDialog$c;->c()Lcom/zopim/android/sdk/attachment/ui/AttachmentSourceSelectorDialog$a;

    move-result-object p1

    invoke-virtual {p1}, Lcom/zopim/android/sdk/attachment/ui/AttachmentSourceSelectorDialog$a;->ordinal()I

    move-result p1

    aget p1, p2, p1

    packed-switch p1, :pswitch_data_0

    goto :goto_1

    :pswitch_0
    sget-object p1, Lcom/zopim/android/sdk/attachment/ImagePicker;->INSTANCE:Lcom/zopim/android/sdk/attachment/ImagePicker;

    iget-object p2, p0, Lcom/zopim/android/sdk/attachment/ui/a;->a:Landroidx/fragment/app/Fragment;

    invoke-virtual {p1, p2}, Lcom/zopim/android/sdk/attachment/ImagePicker;->pickImageFromCamera(Landroidx/fragment/app/Fragment;)V

    goto :goto_0

    :pswitch_1
    sget-object p1, Lcom/zopim/android/sdk/attachment/ImagePicker;->INSTANCE:Lcom/zopim/android/sdk/attachment/ImagePicker;

    iget-object p2, p0, Lcom/zopim/android/sdk/attachment/ui/a;->a:Landroidx/fragment/app/Fragment;

    invoke-virtual {p1, p2}, Lcom/zopim/android/sdk/attachment/ImagePicker;->pickImagesFromGallery(Landroidx/fragment/app/Fragment;)V

    :goto_0
    iget-object p1, p0, Lcom/zopim/android/sdk/attachment/ui/a;->b:Lcom/zopim/android/sdk/attachment/ui/AttachmentSourceSelectorDialog;

    invoke-virtual {p1}, Lcom/zopim/android/sdk/attachment/ui/AttachmentSourceSelectorDialog;->dismiss()V

    :goto_1
    return-void

    nop

    :pswitch_data_0
    .packed-switch 0x1
        :pswitch_1
        :pswitch_0
    .end packed-switch
.end method
