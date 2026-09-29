.class public final enum Lnet/gogame/gowrap/model/news/Article$Category;
.super Ljava/lang/Enum;
.source "Article.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lnet/gogame/gowrap/model/news/Article;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x4019
    name = "Category"
.end annotation

.annotation system Ldalvik/annotation/Signature;
    value = {
        "Ljava/lang/Enum<",
        "Lnet/gogame/gowrap/model/news/Article$Category;",
        ">;"
    }
.end annotation


# static fields
.field private static final synthetic $VALUES:[Lnet/gogame/gowrap/model/news/Article$Category;

.field public static final enum ADMIN:Lnet/gogame/gowrap/model/news/Article$Category;

.field public static final enum EVENT:Lnet/gogame/gowrap/model/news/Article$Category;

.field public static final enum IMPORTANT:Lnet/gogame/gowrap/model/news/Article$Category;

.field public static final enum NOTICE:Lnet/gogame/gowrap/model/news/Article$Category;

.field public static final enum SUMMON:Lnet/gogame/gowrap/model/news/Article$Category;

.field public static final enum TIPS:Lnet/gogame/gowrap/model/news/Article$Category;


# direct methods
.method static constructor <clinit>()V
    .locals 8

    .line 140
    new-instance v0, Lnet/gogame/gowrap/model/news/Article$Category;

    const-string v1, "ADMIN"

    const/4 v2, 0x0

    invoke-direct {v0, v1, v2}, Lnet/gogame/gowrap/model/news/Article$Category;-><init>(Ljava/lang/String;I)V

    sput-object v0, Lnet/gogame/gowrap/model/news/Article$Category;->ADMIN:Lnet/gogame/gowrap/model/news/Article$Category;

    .line 141
    new-instance v0, Lnet/gogame/gowrap/model/news/Article$Category;

    const-string v1, "EVENT"

    const/4 v3, 0x1

    invoke-direct {v0, v1, v3}, Lnet/gogame/gowrap/model/news/Article$Category;-><init>(Ljava/lang/String;I)V

    sput-object v0, Lnet/gogame/gowrap/model/news/Article$Category;->EVENT:Lnet/gogame/gowrap/model/news/Article$Category;

    .line 142
    new-instance v0, Lnet/gogame/gowrap/model/news/Article$Category;

    const-string v1, "IMPORTANT"

    const/4 v4, 0x2

    invoke-direct {v0, v1, v4}, Lnet/gogame/gowrap/model/news/Article$Category;-><init>(Ljava/lang/String;I)V

    sput-object v0, Lnet/gogame/gowrap/model/news/Article$Category;->IMPORTANT:Lnet/gogame/gowrap/model/news/Article$Category;

    .line 143
    new-instance v0, Lnet/gogame/gowrap/model/news/Article$Category;

    const-string v1, "NOTICE"

    const/4 v5, 0x3

    invoke-direct {v0, v1, v5}, Lnet/gogame/gowrap/model/news/Article$Category;-><init>(Ljava/lang/String;I)V

    sput-object v0, Lnet/gogame/gowrap/model/news/Article$Category;->NOTICE:Lnet/gogame/gowrap/model/news/Article$Category;

    .line 144
    new-instance v0, Lnet/gogame/gowrap/model/news/Article$Category;

    const-string v1, "SUMMON"

    const/4 v6, 0x4

    invoke-direct {v0, v1, v6}, Lnet/gogame/gowrap/model/news/Article$Category;-><init>(Ljava/lang/String;I)V

    sput-object v0, Lnet/gogame/gowrap/model/news/Article$Category;->SUMMON:Lnet/gogame/gowrap/model/news/Article$Category;

    .line 145
    new-instance v0, Lnet/gogame/gowrap/model/news/Article$Category;

    const-string v1, "TIPS"

    const/4 v7, 0x5

    invoke-direct {v0, v1, v7}, Lnet/gogame/gowrap/model/news/Article$Category;-><init>(Ljava/lang/String;I)V

    sput-object v0, Lnet/gogame/gowrap/model/news/Article$Category;->TIPS:Lnet/gogame/gowrap/model/news/Article$Category;

    const/4 v0, 0x6

    .line 138
    new-array v0, v0, [Lnet/gogame/gowrap/model/news/Article$Category;

    sget-object v1, Lnet/gogame/gowrap/model/news/Article$Category;->ADMIN:Lnet/gogame/gowrap/model/news/Article$Category;

    aput-object v1, v0, v2

    sget-object v1, Lnet/gogame/gowrap/model/news/Article$Category;->EVENT:Lnet/gogame/gowrap/model/news/Article$Category;

    aput-object v1, v0, v3

    sget-object v1, Lnet/gogame/gowrap/model/news/Article$Category;->IMPORTANT:Lnet/gogame/gowrap/model/news/Article$Category;

    aput-object v1, v0, v4

    sget-object v1, Lnet/gogame/gowrap/model/news/Article$Category;->NOTICE:Lnet/gogame/gowrap/model/news/Article$Category;

    aput-object v1, v0, v5

    sget-object v1, Lnet/gogame/gowrap/model/news/Article$Category;->SUMMON:Lnet/gogame/gowrap/model/news/Article$Category;

    aput-object v1, v0, v6

    sget-object v1, Lnet/gogame/gowrap/model/news/Article$Category;->TIPS:Lnet/gogame/gowrap/model/news/Article$Category;

    aput-object v1, v0, v7

    sput-object v0, Lnet/gogame/gowrap/model/news/Article$Category;->$VALUES:[Lnet/gogame/gowrap/model/news/Article$Category;

    return-void
.end method

.method private constructor <init>(Ljava/lang/String;I)V
    .locals 0
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()V"
        }
    .end annotation

    .line 138
    invoke-direct {p0, p1, p2}, Ljava/lang/Enum;-><init>(Ljava/lang/String;I)V

    return-void
.end method

.method public static valueOf(Ljava/lang/String;)Lnet/gogame/gowrap/model/news/Article$Category;
    .locals 1

    .line 138
    const-class v0, Lnet/gogame/gowrap/model/news/Article$Category;

    invoke-static {v0, p0}, Ljava/lang/Enum;->valueOf(Ljava/lang/Class;Ljava/lang/String;)Ljava/lang/Enum;

    move-result-object p0

    check-cast p0, Lnet/gogame/gowrap/model/news/Article$Category;

    return-object p0
.end method

.method public static values()[Lnet/gogame/gowrap/model/news/Article$Category;
    .locals 1

    .line 138
    sget-object v0, Lnet/gogame/gowrap/model/news/Article$Category;->$VALUES:[Lnet/gogame/gowrap/model/news/Article$Category;

    invoke-virtual {v0}, [Lnet/gogame/gowrap/model/news/Article$Category;->clone()Ljava/lang/Object;

    move-result-object v0

    check-cast v0, [Lnet/gogame/gowrap/model/news/Article$Category;

    return-object v0
.end method
