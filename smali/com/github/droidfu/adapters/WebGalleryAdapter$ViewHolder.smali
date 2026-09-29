.class final Lcom/github/droidfu/adapters/WebGalleryAdapter$ViewHolder;
.super Ljava/lang/Object;
.source "WebGalleryAdapter.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lcom/github/droidfu/adapters/WebGalleryAdapter;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x1a
    name = "ViewHolder"
.end annotation


# instance fields
.field private webImageView:Lcom/github/droidfu/widgets/WebImageView;


# direct methods
.method private constructor <init>()V
    .locals 0

    .line 179
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method

.method synthetic constructor <init>(Lcom/github/droidfu/adapters/WebGalleryAdapter$1;)V
    .locals 0

    .line 179
    invoke-direct {p0}, Lcom/github/droidfu/adapters/WebGalleryAdapter$ViewHolder;-><init>()V

    return-void
.end method

.method static synthetic access$100(Lcom/github/droidfu/adapters/WebGalleryAdapter$ViewHolder;)Lcom/github/droidfu/widgets/WebImageView;
    .locals 0

    .line 179
    iget-object p0, p0, Lcom/github/droidfu/adapters/WebGalleryAdapter$ViewHolder;->webImageView:Lcom/github/droidfu/widgets/WebImageView;

    return-object p0
.end method

.method static synthetic access$102(Lcom/github/droidfu/adapters/WebGalleryAdapter$ViewHolder;Lcom/github/droidfu/widgets/WebImageView;)Lcom/github/droidfu/widgets/WebImageView;
    .locals 0

    .line 179
    iput-object p1, p0, Lcom/github/droidfu/adapters/WebGalleryAdapter$ViewHolder;->webImageView:Lcom/github/droidfu/widgets/WebImageView;

    return-object p1
.end method
