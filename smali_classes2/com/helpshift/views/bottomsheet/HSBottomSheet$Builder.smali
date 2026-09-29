.class public Lcom/helpshift/views/bottomsheet/HSBottomSheet$Builder;
.super Ljava/lang/Object;
.source "HSBottomSheet.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lcom/helpshift/views/bottomsheet/HSBottomSheet;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x9
    name = "Builder"
.end annotation


# instance fields
.field private content:Landroid/view/View;

.field private dimOpacity:F

.field private enableDimAnimation:Z

.field private layoutId:I

.field private layoutWindow:Landroid/view/Window;

.field private referenceView:Landroid/view/View;


# direct methods
.method public constructor <init>(Landroid/view/Window;)V
    .locals 1
    .param p1    # Landroid/view/Window;
        .annotation build Landroidx/annotation/NonNull;
        .end annotation
    .end param

    .line 251
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    const/high16 v0, 0x3f800000    # 1.0f

    .line 242
    iput v0, p0, Lcom/helpshift/views/bottomsheet/HSBottomSheet$Builder;->dimOpacity:F

    .line 252
    iput-object p1, p0, Lcom/helpshift/views/bottomsheet/HSBottomSheet$Builder;->layoutWindow:Landroid/view/Window;

    return-void
.end method


# virtual methods
.method public contentView(I)Lcom/helpshift/views/bottomsheet/HSBottomSheet$Builder;
    .locals 0
    .param p1    # I
        .annotation build Landroidx/annotation/LayoutRes;
        .end annotation
    .end param

    .line 261
    iput p1, p0, Lcom/helpshift/views/bottomsheet/HSBottomSheet$Builder;->layoutId:I

    return-object p0
.end method

.method public dimOpacity(F)Lcom/helpshift/views/bottomsheet/HSBottomSheet$Builder;
    .locals 0

    .line 297
    iput p1, p0, Lcom/helpshift/views/bottomsheet/HSBottomSheet$Builder;->dimOpacity:F

    return-object p0
.end method

.method public enableDimAnimation(Z)Lcom/helpshift/views/bottomsheet/HSBottomSheet$Builder;
    .locals 0

    .line 286
    iput-boolean p1, p0, Lcom/helpshift/views/bottomsheet/HSBottomSheet$Builder;->enableDimAnimation:Z

    return-object p0
.end method

.method public inflateAndBuild()Lcom/helpshift/views/bottomsheet/HSBottomSheet;
    .locals 13
    .annotation build Landroid/annotation/SuppressLint;
        value = {
            "InflateParams"
        }
    .end annotation

    .line 313
    iget-object v0, p0, Lcom/helpshift/views/bottomsheet/HSBottomSheet$Builder;->layoutWindow:Landroid/view/Window;

    if-eqz v0, :cond_1

    .line 321
    iget-boolean v0, p0, Lcom/helpshift/views/bottomsheet/HSBottomSheet$Builder;->enableDimAnimation:Z

    const/4 v1, 0x0

    if-eqz v0, :cond_0

    .line 322
    new-instance v0, Landroid/view/View;

    iget-object v2, p0, Lcom/helpshift/views/bottomsheet/HSBottomSheet$Builder;->referenceView:Landroid/view/View;

    invoke-virtual {v2}, Landroid/view/View;->getContext()Landroid/content/Context;

    move-result-object v2

    invoke-direct {v0, v2}, Landroid/view/View;-><init>(Landroid/content/Context;)V

    .line 323
    iget-object v2, p0, Lcom/helpshift/views/bottomsheet/HSBottomSheet$Builder;->layoutWindow:Landroid/view/Window;

    iget-object v3, p0, Lcom/helpshift/views/bottomsheet/HSBottomSheet$Builder;->layoutWindow:Landroid/view/Window;

    invoke-virtual {v3}, Landroid/view/Window;->getAttributes()Landroid/view/WindowManager$LayoutParams;

    move-result-object v3

    invoke-virtual {v2, v0, v3}, Landroid/view/Window;->addContentView(Landroid/view/View;Landroid/view/ViewGroup$LayoutParams;)V

    move-object v8, v0

    goto :goto_0

    :cond_0
    move-object v8, v1

    .line 326
    :goto_0
    iget-object v0, p0, Lcom/helpshift/views/bottomsheet/HSBottomSheet$Builder;->layoutWindow:Landroid/view/Window;

    invoke-virtual {v0}, Landroid/view/Window;->getContext()Landroid/content/Context;

    move-result-object v0

    invoke-static {v0}, Landroid/view/LayoutInflater;->from(Landroid/content/Context;)Landroid/view/LayoutInflater;

    move-result-object v0

    .line 327
    iget v2, p0, Lcom/helpshift/views/bottomsheet/HSBottomSheet$Builder;->layoutId:I

    invoke-virtual {v0, v2, v1}, Landroid/view/LayoutInflater;->inflate(ILandroid/view/ViewGroup;)Landroid/view/View;

    move-result-object v2

    iput-object v2, p0, Lcom/helpshift/views/bottomsheet/HSBottomSheet$Builder;->content:Landroid/view/View;

    .line 328
    sget v2, Lcom/helpshift/R$layout;->hs__bottomsheet_wrapper:I

    invoke-virtual {v0, v2, v1}, Landroid/view/LayoutInflater;->inflate(ILandroid/view/ViewGroup;)Landroid/view/View;

    move-result-object v0

    move-object v11, v0

    check-cast v11, Landroidx/coordinatorlayout/widget/CoordinatorLayout;

    .line 329
    sget v0, Lcom/helpshift/R$id;->hs__bottom_sheet:I

    invoke-virtual {v11, v0}, Landroidx/coordinatorlayout/widget/CoordinatorLayout;->findViewById(I)Landroid/view/View;

    move-result-object v0

    move-object v12, v0

    check-cast v12, Landroid/widget/FrameLayout;

    .line 330
    new-instance v0, Lcom/helpshift/views/bottomsheet/HSBottomSheet;

    iget-object v5, p0, Lcom/helpshift/views/bottomsheet/HSBottomSheet$Builder;->content:Landroid/view/View;

    iget-object v6, p0, Lcom/helpshift/views/bottomsheet/HSBottomSheet$Builder;->layoutWindow:Landroid/view/Window;

    iget-object v7, p0, Lcom/helpshift/views/bottomsheet/HSBottomSheet$Builder;->referenceView:Landroid/view/View;

    iget-boolean v9, p0, Lcom/helpshift/views/bottomsheet/HSBottomSheet$Builder;->enableDimAnimation:Z

    iget v10, p0, Lcom/helpshift/views/bottomsheet/HSBottomSheet$Builder;->dimOpacity:F

    move-object v4, v0

    invoke-direct/range {v4 .. v12}, Lcom/helpshift/views/bottomsheet/HSBottomSheet;-><init>(Landroid/view/View;Landroid/view/Window;Landroid/view/View;Landroid/view/View;ZFLandroid/view/View;Landroid/view/ViewGroup;)V

    return-object v0

    .line 314
    :cond_1
    new-instance v0, Ljava/lang/IllegalArgumentException;

    const-string v1, "Bottomsheet layout window can not be null"

    invoke-direct {v0, v1}, Ljava/lang/IllegalArgumentException;-><init>(Ljava/lang/String;)V

    throw v0
.end method

.method public referenceView(Landroid/view/View;)Lcom/helpshift/views/bottomsheet/HSBottomSheet$Builder;
    .locals 0
    .param p1    # Landroid/view/View;
        .annotation build Landroidx/annotation/Nullable;
        .end annotation
    .end param

    .line 276
    iput-object p1, p0, Lcom/helpshift/views/bottomsheet/HSBottomSheet$Builder;->referenceView:Landroid/view/View;

    return-object p0
.end method
