.class public Lcom/zopim/android/sdk/attachment/ui/AttachmentSourceSelectorDialog;
.super Landroidx/fragment/app/DialogFragment;


# annotations
.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Lcom/zopim/android/sdk/attachment/ui/AttachmentSourceSelectorDialog$a;,
        Lcom/zopim/android/sdk/attachment/ui/AttachmentSourceSelectorDialog$c;,
        Lcom/zopim/android/sdk/attachment/ui/AttachmentSourceSelectorDialog$b;
    }
.end annotation


# static fields
.field private static final TAG:Ljava/lang/String; = "AttachmentSourceSelectorDialog"


# instance fields
.field private mListView:Landroid/widget/ListView;


# direct methods
.method static constructor <clinit>()V
    .locals 0

    return-void
.end method

.method public constructor <init>()V
    .locals 0

    invoke-direct {p0}, Landroidx/fragment/app/DialogFragment;-><init>()V

    return-void
.end method

.method private fillListView(Landroidx/fragment/app/Fragment;)V
    .locals 6

    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    invoke-virtual {p1}, Landroidx/fragment/app/Fragment;->getActivity()Landroidx/fragment/app/FragmentActivity;

    move-result-object v1

    sget-object v2, Lcom/zopim/android/sdk/attachment/ImagePicker;->INSTANCE:Lcom/zopim/android/sdk/attachment/ImagePicker;

    invoke-virtual {v2, v1}, Lcom/zopim/android/sdk/attachment/ImagePicker;->hasPermissionForCamera(Landroid/content/Context;)Z

    move-result v2

    if-eqz v2, :cond_0

    new-instance v2, Lcom/zopim/android/sdk/attachment/ui/AttachmentSourceSelectorDialog$c;

    sget v3, Lcom/zopim/android/sdk/R$drawable;->ic_chat_action_camera:I

    sget v4, Lcom/zopim/android/sdk/R$string;->attachment_upload_source_camera_button:I

    invoke-virtual {p0, v4}, Lcom/zopim/android/sdk/attachment/ui/AttachmentSourceSelectorDialog;->getString(I)Ljava/lang/String;

    move-result-object v4

    sget-object v5, Lcom/zopim/android/sdk/attachment/ui/AttachmentSourceSelectorDialog$a;->b:Lcom/zopim/android/sdk/attachment/ui/AttachmentSourceSelectorDialog$a;

    invoke-direct {v2, p0, v3, v4, v5}, Lcom/zopim/android/sdk/attachment/ui/AttachmentSourceSelectorDialog$c;-><init>(Lcom/zopim/android/sdk/attachment/ui/AttachmentSourceSelectorDialog;ILjava/lang/String;Lcom/zopim/android/sdk/attachment/ui/AttachmentSourceSelectorDialog$a;)V

    invoke-interface {v0, v2}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    :cond_0
    sget-object v2, Lcom/zopim/android/sdk/attachment/ImagePicker;->INSTANCE:Lcom/zopim/android/sdk/attachment/ImagePicker;

    invoke-virtual {v2, v1}, Lcom/zopim/android/sdk/attachment/ImagePicker;->hasPermissionForGallery(Landroid/content/Context;)Z

    move-result v2

    if-eqz v2, :cond_1

    new-instance v2, Lcom/zopim/android/sdk/attachment/ui/AttachmentSourceSelectorDialog$c;

    sget v3, Lcom/zopim/android/sdk/R$drawable;->ic_chat_action_picture:I

    sget v4, Lcom/zopim/android/sdk/R$string;->attachment_upload_source_gallery_button:I

    invoke-virtual {p0, v4}, Lcom/zopim/android/sdk/attachment/ui/AttachmentSourceSelectorDialog;->getString(I)Ljava/lang/String;

    move-result-object v4

    sget-object v5, Lcom/zopim/android/sdk/attachment/ui/AttachmentSourceSelectorDialog$a;->a:Lcom/zopim/android/sdk/attachment/ui/AttachmentSourceSelectorDialog$a;

    invoke-direct {v2, p0, v3, v4, v5}, Lcom/zopim/android/sdk/attachment/ui/AttachmentSourceSelectorDialog$c;-><init>(Lcom/zopim/android/sdk/attachment/ui/AttachmentSourceSelectorDialog;ILjava/lang/String;Lcom/zopim/android/sdk/attachment/ui/AttachmentSourceSelectorDialog$a;)V

    invoke-interface {v0, v2}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    :cond_1
    invoke-interface {v0}, Ljava/util/List;->size()I

    move-result v2

    const/4 v3, 0x1

    if-ge v2, v3, :cond_2

    sget-object v2, Lcom/zopim/android/sdk/attachment/ui/AttachmentSourceSelectorDialog;->TAG:Ljava/lang/String;

    const-string v3, "No permissions for opening images, dismiss dialog"

    invoke-static {v2, v3}, Lcom/zopim/android/sdk/api/Logger;->d(Ljava/lang/String;Ljava/lang/String;)V

    invoke-virtual {p0}, Lcom/zopim/android/sdk/attachment/ui/AttachmentSourceSelectorDialog;->dismiss()V

    :cond_2
    iget-object v2, p0, Lcom/zopim/android/sdk/attachment/ui/AttachmentSourceSelectorDialog;->mListView:Landroid/widget/ListView;

    new-instance v3, Lcom/zopim/android/sdk/attachment/ui/AttachmentSourceSelectorDialog$b;

    sget v4, Lcom/zopim/android/sdk/R$layout;->row_attachment_source_selector:I

    invoke-direct {v3, p0, v1, v4, v0}, Lcom/zopim/android/sdk/attachment/ui/AttachmentSourceSelectorDialog$b;-><init>(Lcom/zopim/android/sdk/attachment/ui/AttachmentSourceSelectorDialog;Landroid/content/Context;ILjava/util/List;)V

    invoke-virtual {v2, v3}, Landroid/widget/ListView;->setAdapter(Landroid/widget/ListAdapter;)V

    iget-object v0, p0, Lcom/zopim/android/sdk/attachment/ui/AttachmentSourceSelectorDialog;->mListView:Landroid/widget/ListView;

    new-instance v1, Lcom/zopim/android/sdk/attachment/ui/a;

    invoke-direct {v1, p0, p1}, Lcom/zopim/android/sdk/attachment/ui/a;-><init>(Lcom/zopim/android/sdk/attachment/ui/AttachmentSourceSelectorDialog;Landroidx/fragment/app/Fragment;)V

    invoke-virtual {v0, v1}, Landroid/widget/ListView;->setOnItemClickListener(Landroid/widget/AdapterView$OnItemClickListener;)V

    return-void
.end method

.method public static showDialog(Landroidx/fragment/app/FragmentManager;)V
    .locals 2

    new-instance v0, Lcom/zopim/android/sdk/attachment/ui/AttachmentSourceSelectorDialog;

    invoke-direct {v0}, Lcom/zopim/android/sdk/attachment/ui/AttachmentSourceSelectorDialog;-><init>()V

    invoke-virtual {p0}, Landroidx/fragment/app/FragmentManager;->beginTransaction()Landroidx/fragment/app/FragmentTransaction;

    move-result-object p0

    sget-object v1, Lcom/zopim/android/sdk/attachment/ui/AttachmentSourceSelectorDialog;->TAG:Ljava/lang/String;

    invoke-virtual {v0, p0, v1}, Lcom/zopim/android/sdk/attachment/ui/AttachmentSourceSelectorDialog;->show(Landroidx/fragment/app/FragmentTransaction;Ljava/lang/String;)I

    return-void
.end method


# virtual methods
.method public onActivityCreated(Landroid/os/Bundle;)V
    .locals 0

    invoke-super {p0, p1}, Landroidx/fragment/app/DialogFragment;->onActivityCreated(Landroid/os/Bundle;)V

    invoke-virtual {p0}, Lcom/zopim/android/sdk/attachment/ui/AttachmentSourceSelectorDialog;->getParentFragment()Landroidx/fragment/app/Fragment;

    move-result-object p1

    invoke-direct {p0, p1}, Lcom/zopim/android/sdk/attachment/ui/AttachmentSourceSelectorDialog;->fillListView(Landroidx/fragment/app/Fragment;)V

    return-void
.end method

.method public onCreate(Landroid/os/Bundle;)V
    .locals 1
    .param p1    # Landroid/os/Bundle;
        .annotation build Landroidx/annotation/Nullable;
        .end annotation
    .end param

    invoke-super {p0, p1}, Landroidx/fragment/app/DialogFragment;->onCreate(Landroid/os/Bundle;)V

    invoke-virtual {p0}, Lcom/zopim/android/sdk/attachment/ui/AttachmentSourceSelectorDialog;->getTheme()I

    move-result p1

    const/4 v0, 0x1

    invoke-virtual {p0, v0, p1}, Lcom/zopim/android/sdk/attachment/ui/AttachmentSourceSelectorDialog;->setStyle(II)V

    return-void
.end method

.method public onCreateView(Landroid/view/LayoutInflater;Landroid/view/ViewGroup;Landroid/os/Bundle;)Landroid/view/View;
    .locals 1
    .param p2    # Landroid/view/ViewGroup;
        .annotation build Landroidx/annotation/Nullable;
        .end annotation
    .end param
    .param p3    # Landroid/os/Bundle;
        .annotation build Landroidx/annotation/Nullable;
        .end annotation
    .end param

    sget p3, Lcom/zopim/android/sdk/R$layout;->fragment_dialog_attachment_source:I

    const/4 v0, 0x0

    invoke-virtual {p1, p3, p2, v0}, Landroid/view/LayoutInflater;->inflate(ILandroid/view/ViewGroup;Z)Landroid/view/View;

    move-result-object p1

    sget p2, Lcom/zopim/android/sdk/R$id;->dialog_attachment_source_listview:I

    invoke-virtual {p1, p2}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object p2

    check-cast p2, Landroid/widget/ListView;

    iput-object p2, p0, Lcom/zopim/android/sdk/attachment/ui/AttachmentSourceSelectorDialog;->mListView:Landroid/widget/ListView;

    return-object p1
.end method
