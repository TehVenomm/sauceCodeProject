.class final Lnet/gogame/gowrap/ui/utils/UIUtils$1;
.super Ljava/lang/Object;
.source "UIUtils.java"

# interfaces
.implements Landroid/text/TextWatcher;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lnet/gogame/gowrap/ui/utils/UIUtils;->setupRightDrawable(Landroid/content/Context;Landroid/widget/EditText;I)V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x8
    name = null
.end annotation


# instance fields
.field final synthetic val$editText:Landroid/widget/EditText;

.field final synthetic val$emptyDrawableResourceId:I

.field final synthetic val$nonEmptyDrawableResourceId:I


# direct methods
.method constructor <init>(Landroid/widget/EditText;II)V
    .locals 0

    .line 25
    iput-object p1, p0, Lnet/gogame/gowrap/ui/utils/UIUtils$1;->val$editText:Landroid/widget/EditText;

    iput p2, p0, Lnet/gogame/gowrap/ui/utils/UIUtils$1;->val$emptyDrawableResourceId:I

    iput p3, p0, Lnet/gogame/gowrap/ui/utils/UIUtils$1;->val$nonEmptyDrawableResourceId:I

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public afterTextChanged(Landroid/text/Editable;)V
    .locals 0

    return-void
.end method

.method public beforeTextChanged(Ljava/lang/CharSequence;III)V
    .locals 0

    return-void
.end method

.method public onTextChanged(Ljava/lang/CharSequence;III)V
    .locals 0

    .line 34
    iget-object p2, p0, Lnet/gogame/gowrap/ui/utils/UIUtils$1;->val$editText:Landroid/widget/EditText;

    iget p3, p0, Lnet/gogame/gowrap/ui/utils/UIUtils$1;->val$emptyDrawableResourceId:I

    iget p4, p0, Lnet/gogame/gowrap/ui/utils/UIUtils$1;->val$nonEmptyDrawableResourceId:I

    invoke-static {p2, p3, p4, p1}, Lnet/gogame/gowrap/ui/utils/UIUtils;->access$000(Landroid/widget/EditText;IILjava/lang/CharSequence;)V

    return-void
.end method
