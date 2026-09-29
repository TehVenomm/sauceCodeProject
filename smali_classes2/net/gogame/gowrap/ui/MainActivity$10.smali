.class Lnet/gogame/gowrap/ui/MainActivity$10;
.super Ljava/lang/Object;
.source "MainActivity.java"

# interfaces
.implements Landroid/widget/AdapterView$OnItemClickListener;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lnet/gogame/gowrap/ui/MainActivity;->showLanguageMenu()V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lnet/gogame/gowrap/ui/MainActivity;

.field final synthetic val$currentPosition:I

.field final synthetic val$dialog:Landroid/app/Dialog;

.field final synthetic val$languageAdapter:Landroid/widget/ArrayAdapter;


# direct methods
.method constructor <init>(Lnet/gogame/gowrap/ui/MainActivity;ILandroid/widget/ArrayAdapter;Landroid/app/Dialog;)V
    .locals 0

    .line 226
    iput-object p1, p0, Lnet/gogame/gowrap/ui/MainActivity$10;->this$0:Lnet/gogame/gowrap/ui/MainActivity;

    iput p2, p0, Lnet/gogame/gowrap/ui/MainActivity$10;->val$currentPosition:I

    iput-object p3, p0, Lnet/gogame/gowrap/ui/MainActivity$10;->val$languageAdapter:Landroid/widget/ArrayAdapter;

    iput-object p4, p0, Lnet/gogame/gowrap/ui/MainActivity$10;->val$dialog:Landroid/app/Dialog;

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

    .line 230
    iget p1, p0, Lnet/gogame/gowrap/ui/MainActivity$10;->val$currentPosition:I

    if-eq p3, p1, :cond_0

    .line 231
    iget-object p1, p0, Lnet/gogame/gowrap/ui/MainActivity$10;->val$languageAdapter:Landroid/widget/ArrayAdapter;

    .line 232
    invoke-virtual {p1, p3}, Landroid/widget/ArrayAdapter;->getItem(I)Ljava/lang/Object;

    move-result-object p1

    check-cast p1, Lnet/gogame/gowrap/support/LocaleDescriptor;

    .line 233
    iget-object p2, p0, Lnet/gogame/gowrap/ui/MainActivity$10;->this$0:Lnet/gogame/gowrap/ui/MainActivity;

    iget-object p2, p2, Lnet/gogame/gowrap/ui/MainActivity;->localeManager:Lnet/gogame/gowrap/support/LocaleManager;

    invoke-virtual {p1}, Lnet/gogame/gowrap/support/LocaleDescriptor;->getId()Ljava/lang/String;

    move-result-object p1

    invoke-virtual {p2, p1}, Lnet/gogame/gowrap/support/LocaleManager;->setLocale(Ljava/lang/String;)V

    .line 234
    iget-object p1, p0, Lnet/gogame/gowrap/ui/MainActivity$10;->this$0:Lnet/gogame/gowrap/ui/MainActivity;

    invoke-virtual {p1}, Lnet/gogame/gowrap/ui/MainActivity;->recreate()V

    .line 236
    :cond_0
    iget-object p1, p0, Lnet/gogame/gowrap/ui/MainActivity$10;->val$dialog:Landroid/app/Dialog;

    invoke-virtual {p1}, Landroid/app/Dialog;->dismiss()V

    return-void
.end method
