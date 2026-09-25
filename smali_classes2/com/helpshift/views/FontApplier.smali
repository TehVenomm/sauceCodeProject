.class public final Lcom/helpshift/views/FontApplier;
.super Ljava/lang/Object;
.source "FontApplier.java"


# annotations
.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Lcom/helpshift/views/FontApplier$FontLayoutListener;
    }
.end annotation


# static fields
.field public static final TAG:Ljava/lang/String; = "HS_FontApplier"

.field private static typeface:Landroid/graphics/Typeface; = null

.field private static typefaceInitialisationTried:Z = false

.field private static typefaceSpan:Lcom/helpshift/views/HSTypefaceSpan;


# direct methods
.method static constructor <clinit>()V
    .locals 0

    return-void
.end method

.method private constructor <init>()V
    .locals 0

    .line 23
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method

.method public static apply(Landroid/app/Dialog;)V
    .locals 1

    const v0, 0x1020002

    .line 36
    invoke-virtual {p0, v0}, Landroid/app/Dialog;->findViewById(I)Landroid/view/View;

    move-result-object p0

    .line 37
    invoke-static {p0}, Lcom/helpshift/views/FontApplier;->apply(Landroid/view/View;)V

    return-void
.end method

.method public static apply(Landroid/view/View;)V
    .locals 2

    .line 41
    invoke-virtual {p0}, Landroid/view/View;->getContext()Landroid/content/Context;

    move-result-object v0

    invoke-static {v0}, Lcom/helpshift/views/FontApplier;->initTypeface(Landroid/content/Context;)V

    .line 42
    sget-object v0, Lcom/helpshift/views/FontApplier;->typeface:Landroid/graphics/Typeface;

    if-nez v0, :cond_0

    return-void

    .line 45
    :cond_0
    invoke-virtual {p0}, Landroid/view/View;->getViewTreeObserver()Landroid/view/ViewTreeObserver;

    move-result-object v0

    new-instance v1, Lcom/helpshift/views/FontApplier$FontLayoutListener;

    invoke-direct {v1, p0}, Lcom/helpshift/views/FontApplier$FontLayoutListener;-><init>(Landroid/view/View;)V

    invoke-virtual {v0, v1}, Landroid/view/ViewTreeObserver;->addOnGlobalLayoutListener(Landroid/view/ViewTreeObserver$OnGlobalLayoutListener;)V

    return-void
.end method

.method public static apply(Landroid/widget/TextView;)V
    .locals 1

    .line 28
    invoke-virtual {p0}, Landroid/widget/TextView;->getContext()Landroid/content/Context;

    move-result-object v0

    invoke-static {v0}, Lcom/helpshift/views/FontApplier;->initTypeface(Landroid/content/Context;)V

    .line 29
    sget-object v0, Lcom/helpshift/views/FontApplier;->typeface:Landroid/graphics/Typeface;

    if-nez v0, :cond_0

    return-void

    .line 32
    :cond_0
    sget-object v0, Lcom/helpshift/views/FontApplier;->typeface:Landroid/graphics/Typeface;

    invoke-virtual {p0, v0}, Landroid/widget/TextView;->setTypeface(Landroid/graphics/Typeface;)V

    return-void
.end method

.method static applyInternal(Landroid/view/View;)V
    .locals 2

    .line 49
    instance-of v0, p0, Landroid/widget/TextView;

    if-eqz v0, :cond_0

    .line 50
    check-cast p0, Landroid/widget/TextView;

    invoke-static {p0}, Lcom/helpshift/views/FontApplier;->apply(Landroid/widget/TextView;)V

    goto :goto_1

    .line 52
    :cond_0
    instance-of v0, p0, Landroid/view/ViewGroup;

    if-eqz v0, :cond_1

    .line 53
    check-cast p0, Landroid/view/ViewGroup;

    const/4 v0, 0x0

    .line 54
    :goto_0
    invoke-virtual {p0}, Landroid/view/ViewGroup;->getChildCount()I

    move-result v1

    if-ge v0, v1, :cond_1

    .line 55
    invoke-virtual {p0, v0}, Landroid/view/ViewGroup;->getChildAt(I)Landroid/view/View;

    move-result-object v1

    invoke-static {v1}, Lcom/helpshift/views/FontApplier;->applyInternal(Landroid/view/View;)V

    add-int/lit8 v0, v0, 0x1

    goto :goto_0

    :cond_1
    :goto_1
    return-void
.end method

.method public static getFontPath()Ljava/lang/String;
    .locals 1
    .annotation build Landroidx/annotation/Nullable;
    .end annotation

    .line 70
    invoke-static {}, Lcom/helpshift/model/InfoModelFactory;->getInstance()Lcom/helpshift/model/InfoModelFactory;

    move-result-object v0

    iget-object v0, v0, Lcom/helpshift/model/InfoModelFactory;->appInfoModel:Lcom/helpshift/model/AppInfoModel;

    invoke-virtual {v0}, Lcom/helpshift/model/AppInfoModel;->getFontPath()Ljava/lang/String;

    move-result-object v0

    return-object v0
.end method

.method public static getTypefaceSpan()Lcom/helpshift/views/HSTypefaceSpan;
    .locals 2
    .annotation build Landroidx/annotation/Nullable;
    .end annotation

    .line 62
    sget-object v0, Lcom/helpshift/views/FontApplier;->typeface:Landroid/graphics/Typeface;

    if-eqz v0, :cond_0

    sget-object v0, Lcom/helpshift/views/FontApplier;->typefaceSpan:Lcom/helpshift/views/HSTypefaceSpan;

    if-nez v0, :cond_0

    .line 63
    new-instance v0, Lcom/helpshift/views/HSTypefaceSpan;

    sget-object v1, Lcom/helpshift/views/FontApplier;->typeface:Landroid/graphics/Typeface;

    invoke-direct {v0, v1}, Lcom/helpshift/views/HSTypefaceSpan;-><init>(Landroid/graphics/Typeface;)V

    sput-object v0, Lcom/helpshift/views/FontApplier;->typefaceSpan:Lcom/helpshift/views/HSTypefaceSpan;

    .line 65
    :cond_0
    sget-object v0, Lcom/helpshift/views/FontApplier;->typefaceSpan:Lcom/helpshift/views/HSTypefaceSpan;

    return-object v0
.end method

.method private static initTypeface(Landroid/content/Context;)V
    .locals 4

    .line 74
    invoke-static {}, Lcom/helpshift/views/FontApplier;->getFontPath()Ljava/lang/String;

    move-result-object v0

    if-eqz v0, :cond_0

    .line 75
    sget-object v1, Lcom/helpshift/views/FontApplier;->typeface:Landroid/graphics/Typeface;

    if-nez v1, :cond_0

    sget-boolean v1, Lcom/helpshift/views/FontApplier;->typefaceInitialisationTried:Z

    if-nez v1, :cond_0

    const/4 v1, 0x1

    .line 77
    :try_start_0
    invoke-virtual {p0}, Landroid/content/Context;->getAssets()Landroid/content/res/AssetManager;

    move-result-object p0

    invoke-static {p0, v0}, Landroid/graphics/Typeface;->createFromAsset(Landroid/content/res/AssetManager;Ljava/lang/String;)Landroid/graphics/Typeface;

    move-result-object p0

    sput-object p0, Lcom/helpshift/views/FontApplier;->typeface:Landroid/graphics/Typeface;
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    .line 83
    :goto_0
    sput-boolean v1, Lcom/helpshift/views/FontApplier;->typefaceInitialisationTried:Z

    goto :goto_2

    :catchall_0
    move-exception p0

    goto :goto_1

    :catch_0
    move-exception p0

    :try_start_1
    const-string v0, "HS_FontApplier"

    .line 80
    new-instance v2, Ljava/lang/StringBuilder;

    invoke-direct {v2}, Ljava/lang/StringBuilder;-><init>()V

    const-string v3, "Typeface initialisation failed. Using default typeface. "

    invoke-virtual {v2, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p0}, Ljava/lang/Exception;->getMessage()Ljava/lang/String;

    move-result-object p0

    invoke-virtual {v2, p0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v2}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p0

    invoke-static {v0, p0}, Lcom/helpshift/util/HSLogger;->e(Ljava/lang/String;Ljava/lang/String;)V
    :try_end_1
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    goto :goto_0

    .line 83
    :goto_1
    sput-boolean v1, Lcom/helpshift/views/FontApplier;->typefaceInitialisationTried:Z

    .line 84
    throw p0

    :cond_0
    :goto_2
    return-void
.end method
