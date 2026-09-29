.class public Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment$SupportCategoryWrapper;
.super Ljava/lang/Object;
.source "SupportFormFragment.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x9
    name = "SupportCategoryWrapper"
.end annotation


# instance fields
.field private final label:Ljava/lang/String;

.field private final supportCategory:Lnet/gogame/gowrap/support/SupportCategory;


# direct methods
.method public constructor <init>(Lnet/gogame/gowrap/support/SupportCategory;Ljava/lang/String;)V
    .locals 0

    .line 313
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 315
    iput-object p1, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment$SupportCategoryWrapper;->supportCategory:Lnet/gogame/gowrap/support/SupportCategory;

    .line 316
    iput-object p2, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment$SupportCategoryWrapper;->label:Ljava/lang/String;

    return-void
.end method


# virtual methods
.method public getLabel()Ljava/lang/String;
    .locals 1

    .line 324
    iget-object v0, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment$SupportCategoryWrapper;->label:Ljava/lang/String;

    return-object v0
.end method

.method public getSupportCategory()Lnet/gogame/gowrap/support/SupportCategory;
    .locals 1

    .line 320
    iget-object v0, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment$SupportCategoryWrapper;->supportCategory:Lnet/gogame/gowrap/support/SupportCategory;

    return-object v0
.end method

.method public toString()Ljava/lang/String;
    .locals 1

    .line 329
    iget-object v0, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment$SupportCategoryWrapper;->label:Ljava/lang/String;

    return-object v0
.end method
