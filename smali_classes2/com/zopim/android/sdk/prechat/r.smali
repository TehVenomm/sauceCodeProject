.class Lcom/zopim/android/sdk/prechat/r;
.super Ljava/lang/Object;

# interfaces
.implements Landroid/widget/AdapterView$OnItemSelectedListener;


# instance fields
.field final synthetic a:Lcom/zopim/android/sdk/prechat/ZopimPreChatFragment;


# direct methods
.method constructor <init>(Lcom/zopim/android/sdk/prechat/ZopimPreChatFragment;)V
    .locals 0

    iput-object p1, p0, Lcom/zopim/android/sdk/prechat/r;->a:Lcom/zopim/android/sdk/prechat/ZopimPreChatFragment;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public onItemSelected(Landroid/widget/AdapterView;Landroid/view/View;IJ)V
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

    iget-object p1, p0, Lcom/zopim/android/sdk/prechat/r;->a:Lcom/zopim/android/sdk/prechat/ZopimPreChatFragment;

    invoke-static {p1}, Lcom/zopim/android/sdk/prechat/ZopimPreChatFragment;->access$000(Lcom/zopim/android/sdk/prechat/ZopimPreChatFragment;)Landroid/widget/Spinner;

    move-result-object p1

    invoke-virtual {p1}, Landroid/widget/Spinner;->getCount()I

    move-result p1

    add-int/lit8 p1, p1, -0x1

    if-gt p3, p1, :cond_0

    instance-of p1, p2, Landroid/widget/TextView;

    if-eqz p1, :cond_0

    check-cast p2, Landroid/widget/TextView;

    iget-object p1, p0, Lcom/zopim/android/sdk/prechat/r;->a:Lcom/zopim/android/sdk/prechat/ZopimPreChatFragment;

    invoke-virtual {p1}, Lcom/zopim/android/sdk/prechat/ZopimPreChatFragment;->getActivity()Landroidx/fragment/app/FragmentActivity;

    move-result-object p1

    sget p3, Lcom/zopim/android/sdk/R$style;->pre_chat_form_selected_item:I

    invoke-virtual {p2, p1, p3}, Landroid/widget/TextView;->setTextAppearance(Landroid/content/Context;I)V

    :cond_0
    return-void
.end method

.method public onNothingSelected(Landroid/widget/AdapterView;)V
    .locals 0
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Landroid/widget/AdapterView<",
            "*>;)V"
        }
    .end annotation

    return-void
.end method
