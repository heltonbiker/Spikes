using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//public class AlglibMexidoParaRBF
//{
//    public class rbf {

//        public const double eps = 1.0E-6;
//        public const int mxnx = 3;
//        public const double rbffarradius = 6;
//        public const double rbfnearradius = 2.1;
//        public const double rbfmlradius = 3;
//        public const int rbffirstversion = 0;




//        public static void rbfcreate(int nx,
//            int ny,
//            rbfmodel s)
//        {
//            int i = 0;
//            int j = 0;

//            alglib.ap.assert(nx==2 || nx==3, "RBFCreate: NX<>2 and NX<>3");
//            alglib.ap.assert(ny>=1, "RBFCreate: NY<1");
//            s.nx = nx;
//            s.ny = ny;
//            s.nl = 0;
//            s.nc = 0;
//            s.v = new double[ny, mxnx+1];
//            for(i=0; i<=ny-1; i++)
//            {
//                for(j=0; j<=mxnx; j++)
//                {
//                    s.v[i,j] = 0;
//                }
//            }
//            s.n = 0;
//            s.rmax = 0;
//            s.gridtype = 2;
//            s.fixrad = false;
//            s.radvalue = 1;
//            s.radzvalue = 5;
//            s.aterm = 1;
//            s.algorithmtype = 1;
            
//            //
//            // stopping criteria
//            //
//            s.epsort = eps;
//            s.epserr = eps;
//            s.maxits = 0;
//        }


//        public static void rbfsetpoints(rbfmodel s,
//            double[,] xy,
//            int n)
//        {
//            int i = 0;
//            int j = 0;

//            alglib.ap.assert(n>0, "RBFSetPoints: N<0");
//            alglib.ap.assert(alglib.ap.rows(xy)>=n, "RBFSetPoints: Rows(XY)<N");
//            alglib.ap.assert(alglib.ap.cols(xy)>=s.nx+s.ny, "RBFSetPoints: Cols(XY)<NX+NY");
//            s.n = n;
//            s.x = new double[s.n, mxnx];
//            s.y = new double[s.n, s.ny];
//            for(i=0; i<=s.n-1; i++)
//            {
//                for(j=0; j<=mxnx-1; j++)
//                {
//                    s.x[i,j] = 0;
//                }
//                for(j=0; j<=s.nx-1; j++)
//                {
//                    s.x[i,j] = xy[i,j];
//                }
//                for(j=0; j<=s.ny-1; j++)
//                {
//                    s.y[i,j] = xy[i,j+s.nx];
//                }
//            }
//        }


//        public static void rbfsetalgomultilayer(rbfmodel s,
//            double rbase,
//            int nlayers,
//            double lambdav)
//        {
//            alglib.ap.assert(math.isfinite(rbase), "RBFSetAlgoMultiLayer: RBase is infinite or NaN");
//            alglib.ap.assert((double)(rbase)>(double)(0), "RBFSetAlgoMultiLayer: RBase<=0");
//            alglib.ap.assert(nlayers>=0, "RBFSetAlgoMultiLayer: NLayers<0");
//            alglib.ap.assert(math.isfinite(lambdav), "RBFSetAlgoMultiLayer: LambdaV is infinite or NAN");
//            alglib.ap.assert((double)(lambdav)>=(double)(0), "RBFSetAlgoMultiLayer: LambdaV<0");
//            s.radvalue = rbase;
//            s.nlayers = nlayers;
//            s.algorithmtype = 2;
//            s.lambdav = lambdav;
//        }


//        public static void rbfbuildmodel(rbfmodel s,
//            rbfreport rep)
//        {
//            nearestneighbor.kdtree tree = new nearestneighbor.kdtree();
//            nearestneighbor.kdtree ctree = new nearestneighbor.kdtree();
//            double[] dist = new double[0];
//            double[] xcx = new double[0];
//            double[,] a = new double[0,0];
//            double[,] v = new double[0,0];
//            double[,] omega = new double[0,0];
//            double[] y = new double[0];
//            double[,] residualy = new double[0,0];
//            double[] radius = new double[0];
//            double[,] xc = new double[0,0];
//            double[] mnx = new double[0];
//            double[] mxx = new double[0];
//            double[] edge = new double[0];
//            int[] mxsteps = new int[0];
//            int nc = 0;
//            double rmax = 0;
//            int[] tags = new int[0];
//            int[] ctags = new int[0];
//            int i = 0;
//            int j = 0;
//            int k = 0;
//            int k2 = 0;
//            int snnz = 0;
//            double[] tmp0 = new double[0];
//            double[] tmp1 = new double[0];
//            int layerscnt = 0;

//            alglib.ap.assert(s.nx==2 || s.nx==3, "RBFBuildModel: S.NX<>2 or S.NX<>3!");
            
//            //
//            // Quick exit when we have no points
//            //
//            if( s.n==0 )
//            {
//                rep.terminationtype = 1;
//                rep.iterationscount = 0;
//                rep.nmv = 0;
//                rep.arows = 0;
//                rep.acols = 0;
//                nearestneighbor.kdtreebuildtagged(s.xc, tags, 0, mxnx, 0, 2, s.tree);
//                s.xc = new double[0, 0];
//                s.wr = new double[0, 0];
//                s.nc = 0;
//                s.rmax = 0;
//                s.v = new double[s.ny, mxnx+1];
//                for(i=0; i<=s.ny-1; i++)
//                {
//                    for(j=0; j<=mxnx; j++)
//                    {
//                        s.v[i,j] = 0;
//                    }
//                }
//                return;
//            }
            
//            //
//            // General case, N>0
//            //
//            rep.annz = 0;
//            rep.iterationscount = 0;
//            rep.nmv = 0;
//            xcx = new double[mxnx];
            
//            //
//            // First model in a sequence - linear model.
//            // Residuals from linear regression are stored in the ResidualY variable
//            // (used later to build RBF models).
//            //
//            residualy = new double[s.n, s.ny];
//            for(i=0; i<=s.n-1; i++)
//            {
//                for(j=0; j<=s.ny-1; j++)
//                {
//                    residualy[i,j] = s.y[i,j];
//                }
//            }
//            if( !buildlinearmodel(s.x, ref residualy, s.n, s.ny, s.aterm, ref v) )
//            {
//                rep.terminationtype = -5;
//                return;
//            }
            
//            //
//            // Handle special case: multilayer model with NLayers=0.
//            // Quick exit.
//            //
//            if( s.algorithmtype==2 && s.nlayers==0 )
//            {
//                rep.terminationtype = 1;
//                rep.iterationscount = 0;
//                rep.nmv = 0;
//                rep.arows = 0;
//                rep.acols = 0;
//                nearestneighbor.kdtreebuildtagged(s.xc, tags, 0, mxnx, 0, 2, s.tree);
//                s.xc = new double[0, 0];
//                s.wr = new double[0, 0];
//                s.nc = 0;
//                s.rmax = 0;
//                s.v = new double[s.ny, mxnx+1];
//                for(i=0; i<=s.ny-1; i++)
//                {
//                    for(j=0; j<=mxnx; j++)
//                    {
//                        s.v[i,j] = v[i,j];
//                    }
//                }
//                return;
//            }
            
//            //
//            // Second model in a sequence - RBF term.
//            //
//            // NOTE: assignments below are not necessary, but without them
//            //       MSVC complains about unitialized variables.
//            //
//            nc = 0;
//            rmax = 0;
//            layerscnt = 0;
//            if( s.algorithmtype==1 )
//            {
                
//                //
//                // Add RBF model.
//                // This model uses local KD-trees to speed-up nearest neighbor searches.
//                //
//                if( s.gridtype==1 )
//                {
//                    mxx = new double[s.nx];
//                    mnx = new double[s.nx];
//                    mxsteps = new int[s.nx];
//                    edge = new double[s.nx];
//                    for(i=0; i<=s.nx-1; i++)
//                    {
//                        mxx[i] = s.x[0,i];
//                        mnx[i] = s.x[0,i];
//                    }
//                    for(i=0; i<=s.n-1; i++)
//                    {
//                        for(j=0; j<=s.nx-1; j++)
//                        {
//                            if( (double)(mxx[j])<(double)(s.x[i,j]) )
//                            {
//                                mxx[j] = s.x[i,j];
//                            }
//                            if( (double)(mnx[j])>(double)(s.x[i,j]) )
//                            {
//                                mnx[j] = s.x[i,j];
//                            }
//                        }
//                    }
//                    for(i=0; i<=s.nx-1; i++)
//                    {
//                        mxsteps[i] = (int)((mxx[i]-mnx[i])/(2*s.h))+1;
//                        edge[i] = (mxx[i]+mnx[i])/2-s.h*mxsteps[i];
//                    }
//                    nc = 1;
//                    for(i=0; i<=s.nx-1; i++)
//                    {
//                        mxsteps[i] = 2*mxsteps[i]+1;
//                        nc = nc*mxsteps[i];
//                    }
//                    xc = new double[nc, mxnx];
//                    if( s.nx==2 )
//                    {
//                        for(i=0; i<=mxsteps[0]-1; i++)
//                        {
//                            for(j=0; j<=mxsteps[1]-1; j++)
//                            {
//                                for(k2=0; k2<=mxnx-1; k2++)
//                                {
//                                    xc[i*mxsteps[1]+j,k2] = 0;
//                                }
//                                xc[i*mxsteps[1]+j,0] = edge[0]+s.h*i;
//                                xc[i*mxsteps[1]+j,1] = edge[1]+s.h*j;
//                            }
//                        }
//                    }
//                    if( s.nx==3 )
//                    {
//                        for(i=0; i<=mxsteps[0]-1; i++)
//                        {
//                            for(j=0; j<=mxsteps[1]-1; j++)
//                            {
//                                for(k=0; k<=mxsteps[2]-1; k++)
//                                {
//                                    for(k2=0; k2<=mxnx-1; k2++)
//                                    {
//                                        xc[i*mxsteps[1]+j,k2] = 0;
//                                    }
//                                    xc[(i*mxsteps[1]+j)*mxsteps[2]+k,0] = edge[0]+s.h*i;
//                                    xc[(i*mxsteps[1]+j)*mxsteps[2]+k,1] = edge[1]+s.h*j;
//                                    xc[(i*mxsteps[1]+j)*mxsteps[2]+k,2] = edge[2]+s.h*k;
//                                }
//                            }
//                        }
//                    }
//                }
//                else
//                {
//                    if( s.gridtype==2 )
//                    {
//                        nc = s.n;
//                        xc = new double[nc, mxnx];
//                        for(i=0; i<=nc-1; i++)
//                        {
//                            for(j=0; j<=mxnx-1; j++)
//                            {
//                                xc[i,j] = s.x[i,j];
//                            }
//                        }
//                    }
//                    else
//                    {
//                        if( s.gridtype==3 )
//                        {
//                            nc = s.nc;
//                            xc = new double[nc, mxnx];
//                            for(i=0; i<=nc-1; i++)
//                            {
//                                for(j=0; j<=mxnx-1; j++)
//                                {
//                                    xc[i,j] = s.xc[i,j];
//                                }
//                            }
//                        }
//                        else
//                        {
//                            alglib.ap.assert(false, "RBFBuildModel: either S.GridType<1 or S.GridType>3!");
//                        }
//                    }
//                }
//                rmax = 0;
//                radius = new double[nc];
//                ctags = new int[nc];
//                for(i=0; i<=nc-1; i++)
//                {
//                    ctags[i] = i;
//                }
//                nearestneighbor.kdtreebuildtagged(xc, ctags, nc, mxnx, 0, 2, ctree);
//                if( s.fixrad )
//                {
                    
//                    //
//                    // Fixed radius
//                    //
//                    for(i=0; i<=nc-1; i++)
//                    {
//                        radius[i] = s.radvalue;
//                    }
//                    rmax = radius[0];
//                }
//                else
//                {
                    
//                    //
//                    // Dynamic radius
//                    //
//                    if( nc==0 )
//                    {
//                        rmax = 1;
//                    }
//                    else
//                    {
//                        if( nc==1 )
//                        {
//                            radius[0] = s.radvalue;
//                            rmax = radius[0];
//                        }
//                        else
//                        {
                            
//                            //
//                            // NC>1, calculate radii using distances to nearest neigbors
//                            //
//                            for(i=0; i<=nc-1; i++)
//                            {
//                                for(j=0; j<=mxnx-1; j++)
//                                {
//                                    xcx[j] = xc[i,j];
//                                }
//                                if( nearestneighbor.kdtreequeryknn(ctree, xcx, 1, false)>0 )
//                                {
//                                    nearestneighbor.kdtreequeryresultsdistances(ctree, ref dist);
//                                    radius[i] = s.radvalue*dist[0];
//                                }
//                                else
//                                {
                                    
//                                    //
//                                    // No neighbors found (it will happen when we have only one center).
//                                    // Initialize radius with default value.
//                                    //
//                                    radius[i] = 1.0;
//                                }
//                            }
                            
//                            //
//                            // Apply filtering
//                            //
//                            alglib.apserv.rvectorsetlengthatleast(ref tmp0, nc);
//                            for(i=0; i<=nc-1; i++)
//                            {
//                                tmp0[i] = radius[i];
//                            }
//                            alglib.tsort.tagsortfast(ref tmp0, ref tmp1, nc);
//                            for(i=0; i<=nc-1; i++)
//                            {
//                                radius[i] = Math.Min(radius[i], s.radzvalue*tmp0[nc/2]);
//                            }
                            
//                            //
//                            // Calculate RMax, check that all radii are non-zero
//                            //
//                            for(i=0; i<=nc-1; i++)
//                            {
//                                rmax = Math.Max(rmax, radius[i]);
//                            }
//                            for(i=0; i<=nc-1; i++)
//                            {
//                                if( (double)(radius[i])==(double)(0) )
//                                {
//                                    rep.terminationtype = -5;
//                                    return;
//                                }
//                            }
//                        }
//                    }
//                }
//                alglib.apserv.ivectorsetlengthatleast(ref tags, s.n);
//                for(i=0; i<=s.n-1; i++)
//                {
//                    tags[i] = i;
//                }
//                nearestneighbor.kdtreebuildtagged(s.x, tags, s.n, mxnx, 0, 2, tree);
//                buildrbfmodellsqr(s.x, ref residualy, xc, radius, s.n, nc, s.ny, tree, ctree, s.epsort, s.epserr, s.maxits, ref rep.annz, ref snnz, ref omega, ref rep.terminationtype, ref rep.iterationscount, ref rep.nmv);
//                layerscnt = 1;
//            }
//            else
//            {
//                if( s.algorithmtype==2 )
//                {
//                    rmax = s.radvalue;
//                    buildrbfmlayersmodellsqr(s.x, ref residualy, ref xc, s.radvalue, ref radius, s.n, ref nc, s.ny, s.nlayers, ctree, 1.0E-6, 1.0E-6, 50, s.lambdav, ref rep.annz, ref omega, ref rep.terminationtype, ref rep.iterationscount, ref rep.nmv);
//                    layerscnt = s.nlayers;
//                }
//                else
//                {
//                    alglib.ap.assert(false, "RBFBuildModel: internal error(AlgorithmType neither 1 nor 2)");
//                }
//            }
//            if( rep.terminationtype<=0 )
//            {
//                return;
//            }
            
//            //
//            // Model is built
//            //
//            s.nc = nc/layerscnt;
//            s.rmax = rmax;
//            s.nl = layerscnt;
//            s.xc = new double[s.nc, mxnx];
//            s.wr = new double[s.nc, 1+s.nl*s.ny];
//            s.v = new double[s.ny, mxnx+1];
//            for(i=0; i<=s.nc-1; i++)
//            {
//                for(j=0; j<=mxnx-1; j++)
//                {
//                    s.xc[i,j] = xc[i,j];
//                }
//            }
//            apserv.ivectorsetlengthatleast(ref tags, s.nc);
//            for(i=0; i<=s.nc-1; i++)
//            {
//                tags[i] = i;
//            }
//            nearestneighbor.kdtreebuildtagged(s.xc, tags, s.nc, mxnx, 0, 2, s.tree);
//            for(i=0; i<=s.nc-1; i++)
//            {
//                s.wr[i,0] = radius[i];
//                for(k=0; k<=layerscnt-1; k++)
//                {
//                    for(j=0; j<=s.ny-1; j++)
//                    {
//                        s.wr[i,1+k*s.ny+j] = omega[k*s.nc+i,j];
//                    }
//                }
//            }
//            for(i=0; i<=s.ny-1; i++)
//            {
//                for(j=0; j<=mxnx; j++)
//                {
//                    s.v[i,j] = v[i,j];
//                }
//            }
//            rep.terminationtype = 1;
//            rep.arows = s.n;
//            rep.acols = s.nc;
//        }


//        private static bool buildlinearmodel(double[,] x,
//            ref double[,] y,
//            int n,
//            int ny,
//            int modeltype,
//            ref double[,] v)
//        {
//            bool result = new bool();
//            double[] tmpy = new double[0];
//            double[,] a = new double[0,0];
//            double scaling = 0;
//            double[] shifting = new double[0];
//            double mn = 0;
//            double mx = 0;
//            double[] c = new double[0];
//            lsfit.lsfitreport rep = new lsfit.lsfitreport();
//            int i = 0;
//            int j = 0;
//            int k = 0;
//            int info = 0;

//            v = new double[0,0];

//            alglib.ap.assert(n>=0, "BuildLinearModel: N<0");
//            alglib.ap.assert(ny>0, "BuildLinearModel: NY<=0");
            
//            //
//            // Handle degenerate case (N=0)
//            //
//            result = true;
//            v = new double[ny, mxnx+1];
//            if( n==0 )
//            {
//                for(j=0; j<=mxnx; j++)
//                {
//                    for(i=0; i<=ny-1; i++)
//                    {
//                        v[i,j] = 0;
//                    }
//                }
//                return result;
//            }
            
//            //
//            // Allocate temporaries
//            //
//            tmpy = new double[n];
            
//            //
//            // General linear model.
//            //
//            if( modeltype==1 )
//            {
                
//                //
//                // Calculate scaling/shifting, transform variables, prepare LLS problem
//                //
//                a = new double[n, mxnx+1];
//                shifting = new double[mxnx];
//                scaling = 0;
//                for(i=0; i<=mxnx-1; i++)
//                {
//                    mn = x[0,i];
//                    mx = mn;
//                    for(j=1; j<=n-1; j++)
//                    {
//                        if( (double)(mn)>(double)(x[j,i]) )
//                        {
//                            mn = x[j,i];
//                        }
//                        if( (double)(mx)<(double)(x[j,i]) )
//                        {
//                            mx = x[j,i];
//                        }
//                    }
//                    scaling = Math.Max(scaling, mx-mn);
//                    shifting[i] = 0.5*(mx+mn);
//                }
//                if( (double)(scaling)==(double)(0) )
//                {
//                    scaling = 1;
//                }
//                else
//                {
//                    scaling = 0.5*scaling;
//                }
//                for(i=0; i<=n-1; i++)
//                {
//                    for(j=0; j<=mxnx-1; j++)
//                    {
//                        a[i,j] = (x[i,j]-shifting[j])/scaling;
//                    }
//                }
//                for(i=0; i<=n-1; i++)
//                {
//                    a[i,mxnx] = 1;
//                }
                
//                //
//                // Solve linear system in transformed variables, make backward 
//                //
//                for(i=0; i<=ny-1; i++)
//                {
//                    for(j=0; j<=n-1; j++)
//                    {
//                        tmpy[j] = y[j,i];
//                    }
//                    lsfit.lsfitlinear(tmpy, a, n, mxnx+1, ref info, ref c, rep);
//                    if( info<=0 )
//                    {
//                        result = false;
//                        return result;
//                    }
//                    for(j=0; j<=mxnx-1; j++)
//                    {
//                        v[i,j] = c[j]/scaling;
//                    }
//                    v[i,mxnx] = c[mxnx];
//                    for(j=0; j<=mxnx-1; j++)
//                    {
//                        v[i,mxnx] = v[i,mxnx]-shifting[j]*v[i,j];
//                    }
//                    for(j=0; j<=n-1; j++)
//                    {
//                        for(k=0; k<=mxnx-1; k++)
//                        {
//                            y[j,i] = y[j,i]-x[j,k]*v[i,k];
//                        }
//                        y[j,i] = y[j,i]-v[i,mxnx];
//                    }
//                }
//                return result;
//            }
            
//            //
//            // Constant model, very simple
//            //
//            if( modeltype==2 )
//            {
//                for(i=0; i<=ny-1; i++)
//                {
//                    for(j=0; j<=mxnx; j++)
//                    {
//                        v[i,j] = 0;
//                    }
//                    for(j=0; j<=n-1; j++)
//                    {
//                        v[i,mxnx] = v[i,mxnx]+y[j,i];
//                    }
//                    if( n>0 )
//                    {
//                        v[i,mxnx] = v[i,mxnx]/n;
//                    }
//                    for(j=0; j<=n-1; j++)
//                    {
//                        y[j,i] = y[j,i]-v[i,mxnx];
//                    }
//                }
//                return result;
//            }
            
//            //
//            // Zero model
//            //
//            alglib.ap.assert(modeltype==3, "BuildLinearModel: unknown model type");
//            for(i=0; i<=ny-1; i++)
//            {
//                for(j=0; j<=mxnx; j++)
//                {
//                    v[i,j] = 0;
//                }
//            }
//            return result;
//        }


//        private static void buildrbfmodellsqr(double[,] x,
//            ref double[,] y,
//            double[,] xc,
//            double[] r,
//            int n,
//            int nc,
//            int ny,
//            nearestneighbor.kdtree pointstree,
//            nearestneighbor.kdtree centerstree,
//            double epsort,
//            double epserr,
//            int maxits,
//            ref int gnnz,
//            ref int snnz,
//            ref double[,] w,
//            ref int info,
//            ref int iterationscount,
//            ref int nmv)
//        {
//            linlsqr.linlsqrstate state = new linlsqr.linlsqrstate();
//            linlsqr.linlsqrreport lsqrrep = new linlsqr.linlsqrreport();
//            sparse.sparsematrix spg = new sparse.sparsematrix();
//            sparse.sparsematrix sps = new sparse.sparsematrix();
//            int[] nearcenterscnt = new int[0];
//            int[] nearpointscnt = new int[0];
//            int[] skipnearpointscnt = new int[0];
//            int[] farpointscnt = new int[0];
//            int maxnearcenterscnt = 0;
//            int maxnearpointscnt = 0;
//            int maxfarpointscnt = 0;
//            int sumnearcenterscnt = 0;
//            int sumnearpointscnt = 0;
//            int sumfarpointscnt = 0;
//            double maxrad = 0;
//            int[] pointstags = new int[0];
//            int[] centerstags = new int[0];
//            double[,] nearpoints = new double[0,0];
//            double[,] nearcenters = new double[0,0];
//            double[,] farpoints = new double[0,0];
//            int tmpi = 0;
//            int pointscnt = 0;
//            int centerscnt = 0;
//            double[] xcx = new double[0];
//            double[] tmpy = new double[0];
//            double[] tc = new double[0];
//            double[] g = new double[0];
//            double[] c = new double[0];
//            int i = 0;
//            int j = 0;
//            int k = 0;
//            int sind = 0;
//            double[,] a = new double[0,0];
//            double vv = 0;
//            double vx = 0;
//            double vy = 0;
//            double vz = 0;
//            double vr = 0;
//            double gnorm2 = 0;
//            double[] tmp0 = new double[0];
//            double[] tmp1 = new double[0];
//            double[] tmp2 = new double[0];
//            double fx = 0;
//            double[,] xx = new double[0,0];
//            double[,] cx = new double[0,0];
//            double mrad = 0;
//            int i_ = 0;

//            gnnz = 0;
//            snnz = 0;
//            w = new double[0,0];
//            info = 0;
//            iterationscount = 0;
//            nmv = 0;

            
//            //
//            // Handle special cases: NC=0
//            //
//            if( nc==0 )
//            {
//                info = 1;
//                iterationscount = 0;
//                nmv = 0;
//                return;
//            }
            
//            //
//            // Prepare for general case, NC>0
//            //
//            xcx = new double[mxnx];
//            pointstags = new int[n];
//            centerstags = new int[nc];
//            info = -1;
//            iterationscount = 0;
//            nmv = 0;
            
//            //
//            // This block prepares quantities used to compute approximate cardinal basis functions (ACBFs):
//            // * NearCentersCnt[]   -   array[NC], whose elements store number of near centers used to build ACBF
//            // * NearPointsCnt[]    -   array[NC], number of near points used to build ACBF
//            // * FarPointsCnt[]     -   array[NC], number of far points (ones where ACBF is nonzero)
//            // * MaxNearCentersCnt  -   max(NearCentersCnt)
//            // * MaxNearPointsCnt   -   max(NearPointsCnt)
//            // * SumNearCentersCnt  -   sum(NearCentersCnt)
//            // * SumNearPointsCnt   -   sum(NearPointsCnt)
//            // * SumFarPointsCnt    -   sum(FarPointsCnt)
//            //
//            nearcenterscnt = new int[nc];
//            nearpointscnt = new int[nc];
//            skipnearpointscnt = new int[nc];
//            farpointscnt = new int[nc];
//            maxnearcenterscnt = 0;
//            maxnearpointscnt = 0;
//            maxfarpointscnt = 0;
//            sumnearcenterscnt = 0;
//            sumnearpointscnt = 0;
//            sumfarpointscnt = 0;
//            for(i=0; i<=nc-1; i++)
//            {
//                for(j=0; j<=mxnx-1; j++)
//                {
//                    xcx[j] = xc[i,j];
//                }
                
//                //
//                // Determine number of near centers and maximum radius of near centers
//                //
//                nearcenterscnt[i] = nearestneighbor.kdtreequeryrnn(centerstree, xcx, r[i]*rbfnearradius, true);
//                nearestneighbor.kdtreequeryresultstags(centerstree, ref centerstags);
//                maxrad = 0;
//                for(j=0; j<=nearcenterscnt[i]-1; j++)
//                {
//                    maxrad = Math.Max(maxrad, Math.Abs(r[centerstags[j]]));
//                }
                
//                //
//                // Determine number of near points (ones which used to build ACBF)
//                // and skipped points (the most near points which are NOT used to build ACBF
//                // and are NOT included in the near points count
//                //
//                skipnearpointscnt[i] = nearestneighbor.kdtreequeryrnn(pointstree, xcx, 0.1*r[i], true);
//                nearpointscnt[i] = nearestneighbor.kdtreequeryrnn(pointstree, xcx, (r[i]+maxrad)*rbfnearradius, true)-skipnearpointscnt[i];
//                alglib.ap.assert(nearpointscnt[i]>=0, "BuildRBFModelLSQR: internal error");
                
//                //
//                // Determine number of far points
//                //
//                farpointscnt[i] = nearestneighbor.kdtreequeryrnn(pointstree, xcx, Math.Max(r[i]*rbfnearradius+maxrad*rbffarradius, r[i]*rbffarradius), true);
                
//                //
//                // calculate sum and max, make some basic checks
//                //
//                alglib.ap.assert(nearcenterscnt[i]>0, "BuildRBFModelLSQR: internal error");
//                maxnearcenterscnt = Math.Max(maxnearcenterscnt, nearcenterscnt[i]);
//                maxnearpointscnt = Math.Max(maxnearpointscnt, nearpointscnt[i]);
//                maxfarpointscnt = Math.Max(maxfarpointscnt, farpointscnt[i]);
//                sumnearcenterscnt = sumnearcenterscnt+nearcenterscnt[i];
//                sumnearpointscnt = sumnearpointscnt+nearpointscnt[i];
//                sumfarpointscnt = sumfarpointscnt+farpointscnt[i];
//            }
//            snnz = sumnearcenterscnt;
//            gnnz = sumfarpointscnt;
//            alglib.ap.assert(maxnearcenterscnt>0, "BuildRBFModelLSQR: internal error");
            
//            //
//            // Allocate temporaries.
//            //
//            // NOTE: we want to avoid allocation of zero-size arrays, so we
//            //       use max(desired_size,1) instead of desired_size when performing
//            //       memory allocation.
//            //
//            a = new double[maxnearpointscnt+maxnearcenterscnt, maxnearcenterscnt];
//            tmpy = new double[maxnearpointscnt+maxnearcenterscnt];
//            g = new double[maxnearcenterscnt];
//            c = new double[maxnearcenterscnt];
//            nearcenters = new double[maxnearcenterscnt, mxnx];
//            nearpoints = new double[Math.Max(maxnearpointscnt, 1), mxnx];
//            farpoints = new double[Math.Max(maxfarpointscnt, 1), mxnx];
            
//            //
//            // fill matrix SpG
//            //
//            sparse.sparsecreate(n, nc, gnnz, spg);
//            sparse.sparsecreate(nc, nc, snnz, sps);
//            for(i=0; i<=nc-1; i++)
//            {
//                centerscnt = nearcenterscnt[i];
                
//                //
//                // main center
//                //
//                for(j=0; j<=mxnx-1; j++)
//                {
//                    xcx[j] = xc[i,j];
//                }
                
//                //
//                // center's tree
//                //
//                tmpi = nearestneighbor.kdtreequeryknn(centerstree, xcx, centerscnt, true);
//                alglib.ap.assert(tmpi==centerscnt, "BuildRBFModelLSQR: internal error");
//                nearestneighbor.kdtreequeryresultsx(centerstree, ref cx);
//                nearestneighbor.kdtreequeryresultstags(centerstree, ref centerstags);
                
//                //
//                // point's tree
//                //
//                mrad = 0;
//                for(j=0; j<=centerscnt-1; j++)
//                {
//                    mrad = Math.Max(mrad, r[centerstags[j]]);
//                }
                
//                //
//                // we need to be sure that 'CTree' contains
//                // at least one side center
//                //
//                sparse.sparseset(sps, i, i, 1);
//                c[0] = 1.0;
//                for(j=1; j<=centerscnt-1; j++)
//                {
//                    c[j] = 0.0;
//                }
//                if( centerscnt>1 && nearpointscnt[i]>0 )
//                {
                    
//                    //
//                    // first KDTree request for points
//                    //
//                    pointscnt = nearpointscnt[i];
//                    tmpi = nearestneighbor.kdtreequeryknn(pointstree, xcx, skipnearpointscnt[i]+nearpointscnt[i], true);
//                    alglib.ap.assert(tmpi==skipnearpointscnt[i]+nearpointscnt[i], "BuildRBFModelLSQR: internal error");
//                    nearestneighbor.kdtreequeryresultsx(pointstree, ref xx);
//                    sind = skipnearpointscnt[i];
//                    for(j=0; j<=pointscnt-1; j++)
//                    {
//                        vx = xx[sind+j,0];
//                        vy = xx[sind+j,1];
//                        vz = xx[sind+j,2];
//                        for(k=0; k<=centerscnt-1; k++)
//                        {
//                            vr = 0.0;
//                            vv = vx-cx[k,0];
//                            vr = vr+vv*vv;
//                            vv = vy-cx[k,1];
//                            vr = vr+vv*vv;
//                            vv = vz-cx[k,2];
//                            vr = vr+vv*vv;
//                            vv = r[centerstags[k]];
//                            a[j,k] = Math.Exp(-(vr/(vv*vv)));
//                        }
//                    }
//                    for(j=0; j<=centerscnt-1; j++)
//                    {
//                        g[j] = Math.Exp(-((math.sqr(xcx[0]-cx[j,0])+math.sqr(xcx[1]-cx[j,1])+math.sqr(xcx[2]-cx[j,2]))/math.sqr(r[centerstags[j]])));
//                    }
                    
//                    //
//                    // calculate the problem
//                    //
//                    gnorm2 = 0.0;
//                    for(i_=0; i_<=centerscnt-1;i_++)
//                    {
//                        gnorm2 += g[i_]*g[i_];
//                    }
//                    for(j=0; j<=pointscnt-1; j++)
//                    {
//                        vv = 0.0;
//                        for(i_=0; i_<=centerscnt-1;i_++)
//                        {
//                            vv += a[j,i_]*g[i_];
//                        }
//                        vv = vv/gnorm2;
//                        tmpy[j] = -vv;
//                        for(i_=0; i_<=centerscnt-1;i_++)
//                        {
//                            a[j,i_] = a[j,i_] - vv*g[i_];
//                        }
//                    }
//                    for(j=pointscnt; j<=pointscnt+centerscnt-1; j++)
//                    {
//                        for(k=0; k<=centerscnt-1; k++)
//                        {
//                            a[j,k] = 0.0;
//                        }
//                        a[j,j-pointscnt] = 1.0E-6;
//                        tmpy[j] = 0.0;
//                    }
//                    fbls.fblssolvels(ref a, ref tmpy, pointscnt+centerscnt, centerscnt, ref tmp0, ref tmp1, ref tmp2);
//                    for(i_=0; i_<=centerscnt-1;i_++)
//                    {
//                        c[i_] = tmpy[i_];
//                    }
//                    vv = 0.0;
//                    for(i_=0; i_<=centerscnt-1;i_++)
//                    {
//                        vv += g[i_]*c[i_];
//                    }
//                    vv = vv/gnorm2;
//                    for(i_=0; i_<=centerscnt-1;i_++)
//                    {
//                        c[i_] = c[i_] - vv*g[i_];
//                    }
//                    vv = 1/gnorm2;
//                    for(i_=0; i_<=centerscnt-1;i_++)
//                    {
//                        c[i_] = c[i_] + vv*g[i_];
//                    }
//                    for(j=0; j<=centerscnt-1; j++)
//                    {
//                        sparse.sparseset(sps, i, centerstags[j], c[j]);
//                    }
//                }
                
//                //
//                // second KDTree request for points
//                //
//                pointscnt = farpointscnt[i];
//                tmpi = nearestneighbor.kdtreequeryknn(pointstree, xcx, pointscnt, true);
//                alglib.ap.assert(tmpi==pointscnt, "BuildRBFModelLSQR: internal error");
//                nearestneighbor.kdtreequeryresultsx(pointstree, ref xx);
//                nearestneighbor.kdtreequeryresultstags(pointstree, ref pointstags);
                
//                //
//                //fill SpG matrix
//                //
//                for(j=0; j<=pointscnt-1; j++)
//                {
//                    fx = 0;
//                    vx = xx[j,0];
//                    vy = xx[j,1];
//                    vz = xx[j,2];
//                    for(k=0; k<=centerscnt-1; k++)
//                    {
//                        vr = 0.0;
//                        vv = vx-cx[k,0];
//                        vr = vr+vv*vv;
//                        vv = vy-cx[k,1];
//                        vr = vr+vv*vv;
//                        vv = vz-cx[k,2];
//                        vr = vr+vv*vv;
//                        vv = r[centerstags[k]];
//                        vv = vv*vv;
//                        fx = fx+c[k]*Math.Exp(-(vr/vv));
//                    }
//                    sparse.sparseset(spg, pointstags[j], i, fx);
//                }
//            }
//            sparse.sparseconverttocrs(spg);
//            sparse.sparseconverttocrs(sps);
            
//            //
//            // solve by LSQR method
//            //
//            tmpy = new double[n];
//            tc = new double[nc];
//            w = new double[nc, ny];
//            linlsqr.linlsqrcreate(n, nc, state);
//            linlsqr.linlsqrsetcond(state, epsort, epserr, maxits);
//            for(i=0; i<=ny-1; i++)
//            {
//                for(j=0; j<=n-1; j++)
//                {
//                    tmpy[j] = y[j,i];
//                }
//                linlsqr.linlsqrsolvesparse(state, spg, tmpy);
//                linlsqr.linlsqrresults(state, ref c, lsqrrep);
//                if( lsqrrep.terminationtype<=0 )
//                {
//                    info = -4;
//                    return;
//                }
//                sparse.sparsemtv(sps, c, ref tc);
//                for(j=0; j<=nc-1; j++)
//                {
//                    w[j,i] = tc[j];
//                }
//                iterationscount = iterationscount+lsqrrep.iterationscount;
//                nmv = nmv+lsqrrep.nmv;
//            }
//            info = 1;
//        }


//        private static void buildrbfmlayersmodellsqr(double[,] x,
//            ref double[,] y,
//            ref double[,] xc,
//            double rval,
//            ref double[] r,
//            int n,
//            ref int nc,
//            int ny,
//            int nlayers,
//            nearestneighbor.kdtree centerstree,
//            double epsort,
//            double epserr,
//            int maxits,
//            double lambdav,
//            ref int annz,
//            ref double[,] w,
//            ref int info,
//            ref int iterationscount,
//            ref int nmv)
//        {
//            linlsqr.linlsqrstate state = new linlsqr.linlsqrstate();
//            linlsqr.linlsqrreport lsqrrep = new linlsqr.linlsqrreport();
//            sparse.sparsematrix spa = new sparse.sparsematrix();
//            double anorm = 0;
//            double[] omega = new double[0];
//            double[] xx = new double[0];
//            double[] tmpy = new double[0];
//            double[,] cx = new double[0,0];
//            double yval = 0;
//            int nec = 0;
//            int[] centerstags = new int[0];
//            int layer = 0;
//            int i = 0;
//            int j = 0;
//            int k = 0;
//            double v = 0;
//            double rmaxbefore = 0;
//            double rmaxafter = 0;

//            xc = new double[0,0];
//            r = new double[0];
//            nc = 0;
//            annz = 0;
//            w = new double[0,0];
//            info = 0;
//            iterationscount = 0;
//            nmv = 0;

//            alglib.ap.assert(nlayers>=0, "BuildRBFMLayersModelLSQR: invalid argument(NLayers<0)");
//            alglib.ap.assert(n>=0, "BuildRBFMLayersModelLSQR: invalid argument(N<0)");
//            alglib.ap.assert(mxnx>0 && mxnx<=3, "BuildRBFMLayersModelLSQR: internal error(invalid global const MxNX: either MxNX<=0 or MxNX>3)");
//            annz = 0;
//            if( n==0 || nlayers==0 )
//            {
//                info = 1;
//                iterationscount = 0;
//                nmv = 0;
//                return;
//            }
//            nc = n*nlayers;
//            xx = new double[mxnx];
//            centerstags = new int[n];
//            xc = new double[nc, mxnx];
//            r = new double[nc];
//            for(i=0; i<=nc-1; i++)
//            {
//                for(j=0; j<=mxnx-1; j++)
//                {
//                    xc[i,j] = x[i%n,j];
//                }
//            }
//            for(i=0; i<=nc-1; i++)
//            {
//                r[i] = rval/Math.Pow(2, i/n);
//            }
//            for(i=0; i<=n-1; i++)
//            {
//                centerstags[i] = i;
//            }
//            nearestneighbor.kdtreebuildtagged(xc, centerstags, n, mxnx, 0, 2, centerstree);
//            omega = new double[n];
//            tmpy = new double[n];
//            w = new double[nc, ny];
//            info = -1;
//            iterationscount = 0;
//            nmv = 0;
//            linlsqr.linlsqrcreate(n, n, state);
//            linlsqr.linlsqrsetcond(state, epsort, epserr, maxits);
//            linlsqr.linlsqrsetlambdai(state, 1.0E-6);
            
//            //
//            // calculate number of non-zero elements for sparse matrix
//            //
//            for(i=0; i<=n-1; i++)
//            {
//                for(j=0; j<=mxnx-1; j++)
//                {
//                    xx[j] = x[i,j];
//                }
//                annz = annz+nearestneighbor.kdtreequeryrnn(centerstree, xx, r[0]*rbfmlradius, true);
//            }
//            for(layer=0; layer<=nlayers-1; layer++)
//            {
                
//                //
//                // Fill sparse matrix, calculate norm(A)
//                //
//                anorm = 0.0;
//                sparse.sparsecreate(n, n, annz, spa);
//                for(i=0; i<=n-1; i++)
//                {
//                    for(j=0; j<=mxnx-1; j++)
//                    {
//                        xx[j] = x[i,j];
//                    }
//                    nec = nearestneighbor.kdtreequeryrnn(centerstree, xx, r[layer*n]*rbfmlradius, true);
//                    nearestneighbor.kdtreequeryresultsx(centerstree, ref cx);
//                    nearestneighbor.kdtreequeryresultstags(centerstree, ref centerstags);
//                    for(j=0; j<=nec-1; j++)
//                    {
//                        v = Math.Exp(-((math.sqr(xx[0]-cx[j,0])+math.sqr(xx[1]-cx[j,1])+math.sqr(xx[2]-cx[j,2]))/math.sqr(r[layer*n+centerstags[j]])));
//                        sparse.sparseset(spa, i, centerstags[j], v);
//                        anorm = anorm+math.sqr(v);
//                    }
//                }
//                anorm = Math.Sqrt(anorm);
//                sparse.sparseconverttocrs(spa);
                
//                //
//                // Calculate maximum residual before adding new layer.
//                // This value is not used by algorithm, the only purpose is to make debugging easier.
//                //
//                rmaxbefore = 0.0;
//                for(j=0; j<=n-1; j++)
//                {
//                    for(i=0; i<=ny-1; i++)
//                    {
//                        rmaxbefore = Math.Max(rmaxbefore, Math.Abs(y[j,i]));
//                    }
//                }
                
//                //
//                // Process NY dimensions of the target function
//                //
//                for(i=0; i<=ny-1; i++)
//                {
//                    for(j=0; j<=n-1; j++)
//                    {
//                        tmpy[j] = y[j,i];
//                    }
                    
//                    //
//                    // calculate Omega for current layer
//                    //
//                    linlsqr.linlsqrsetlambdai(state, lambdav*anorm/n);
//                    linlsqr.linlsqrsolvesparse(state, spa, tmpy);
//                    linlsqr.linlsqrresults(state, ref omega, lsqrrep);
//                    if( lsqrrep.terminationtype<=0 )
//                    {
//                        info = -4;
//                        return;
//                    }
                    
//                    //
//                    // calculate error for current layer
//                    //
//                    for(j=0; j<=n-1; j++)
//                    {
//                        yval = 0;
//                        for(k=0; k<=mxnx-1; k++)
//                        {
//                            xx[k] = x[j,k];
//                        }
//                        nec = nearestneighbor.kdtreequeryrnn(centerstree, xx, r[layer*n]*rbffarradius, true);
//                        nearestneighbor.kdtreequeryresultsx(centerstree, ref cx);
//                        nearestneighbor.kdtreequeryresultstags(centerstree, ref centerstags);
//                        for(k=0; k<=nec-1; k++)
//                        {
//                            yval = yval+omega[centerstags[k]]*Math.Exp(-((math.sqr(xx[0]-cx[k,0])+math.sqr(xx[1]-cx[k,1])+math.sqr(xx[2]-cx[k,2]))/math.sqr(r[layer*n+centerstags[k]])));
//                        }
//                        y[j,i] = y[j,i]-yval;
//                    }
                    
//                    //
//                    // write Omega in out parameter W
//                    //
//                    for(j=0; j<=n-1; j++)
//                    {
//                        w[layer*n+j,i] = omega[j];
//                    }
//                    iterationscount = iterationscount+lsqrrep.iterationscount;
//                    nmv = nmv+lsqrrep.nmv;
//                }
                
//                //
//                // Calculate maximum residual before adding new layer.
//                // This value is not used by algorithm, the only purpose is to make debugging easier.
//                //
//                rmaxafter = 0.0;
//                for(j=0; j<=n-1; j++)
//                {
//                    for(i=0; i<=ny-1; i++)
//                    {
//                        rmaxafter = Math.Max(rmaxafter, Math.Abs(y[j,i]));
//                    }
//                }
//            }
//            info = 1;
//        }








//        public class rbfmodel : algopascalobject
//        {
//            public int ny;
//            public int nx;
//            public int nc;
//            public int nl;
//            public nearestneighbor.kdtree tree;
//            public double[,] xc;
//            public double[,] wr;
//            public double rmax;
//            public double[,] v;
//            public int gridtype;
//            public bool fixrad;
//            public double lambdav;
//            public double radvalue;
//            public double radzvalue;
//            public int nlayers;
//            public int aterm;
//            public int algorithmtype;
//            public double epsort;
//            public double epserr;
//            public int maxits;
//            public double h;
//            public int n;
//            public double[,] x;
//            public double[,] y;
//            public double[] calcbufxcx;
//            public double[,] calcbufx;
//            public int[] calcbuftags;
//            public rbfmodel()
//            {
//                init();
//            }
//            public override void init()
//            {
//                tree = new nearestneighbor.kdtree();
//                xc = new double[0,0];
//                wr = new double[0,0];
//                v = new double[0,0];
//                x = new double[0,0];
//                y = new double[0,0];
//                calcbufxcx = new double[0];
//                calcbufx = new double[0,0];
//                calcbuftags = new int[0];
//            }
//            public override algopascalobject make_copy()
//            {
//                rbfmodel _result = new rbfmodel();
//                _result.ny = ny;
//                _result.nx = nx;
//                _result.nc = nc;
//                _result.nl = nl;
//                _result.tree = (nearestneighbor.kdtree)tree.make_copy();
//                _result.xc = (double[,])xc.Clone();
//                _result.wr = (double[,])wr.Clone();
//                _result.rmax = rmax;
//                _result.v = (double[,])v.Clone();
//                _result.gridtype = gridtype;
//                _result.fixrad = fixrad;
//                _result.lambdav = lambdav;
//                _result.radvalue = radvalue;
//                _result.radzvalue = radzvalue;
//                _result.nlayers = nlayers;
//                _result.aterm = aterm;
//                _result.algorithmtype = algorithmtype;
//                _result.epsort = epsort;
//                _result.epserr = epserr;
//                _result.maxits = maxits;
//                _result.h = h;
//                _result.n = n;
//                _result.x = (double[,])x.Clone();
//                _result.y = (double[,])y.Clone();
//                _result.calcbufxcx = (double[])calcbufxcx.Clone();
//                _result.calcbufx = (double[,])calcbufx.Clone();
//                _result.calcbuftags = (int[])calcbuftags.Clone();
//                return _result;
//            }
//        };


//        public class rbfreport : algopascalobject
//        {
//            public int arows;
//            public int acols;
//            public int annz;
//            public int iterationscount;
//            public int nmv;
//            public int terminationtype;
//            public rbfreport()
//            {
//                init();
//            }
//            public override void init()
//            {
//            }
//            public override algopascalobject make_copy()
//            {
//                rbfreport _result = new rbfreport();
//                _result.arows = arows;
//                _result.acols = acols;
//                _result.annz = annz;
//                _result.iterationscount = iterationscount;
//                _result.nmv = nmv;
//                _result.terminationtype = terminationtype;
//                return _result;
//            }
//        };



//    }







//    public static void rbfbuildmodel(rbfmodel s, out rbfreport rep)
//    {
//        rep = new rbfreport();
//        rbf.rbfbuildmodel(s.innerobj, rep.innerobj);
//        return;
//    }





//    public static bool isfinitevector(double[] x,
//        int n)
//    {
//        bool result = new bool();
//        int i = 0;

//        alglib.ap.assert(n>=0, "APSERVIsFiniteVector: internal error (N<0)");
//        if( n==0 )
//        {
//            result = true;
//            return result;
//        }
//        if( alglib.ap.len(x)<n )
//        {
//            result = false;
//            return result;
//        }
//        for(i=0; i<=n-1; i++)
//        {
//            if( !math.isfinite(x[i]) )
//            {
//                result = false;
//                return result;
//            }
//        }
//        result = true;
//        return result;
//    }




//    public class rbfmodel : alglibobject
//    {
//        //
//        // Public declarations
//        //

//        public rbfmodel()
//        {
//            _innerobj = new rbf.rbfmodel();
//        }

//        //
//        // Although some of declarations below are public, you should not use them
//        // They are intended for internal use only
//        //
//        private rbf.rbfmodel _innerobj;
//        public rbf.rbfmodel innerobj { get { return _innerobj; } }
//        public rbfmodel(rbf.rbfmodel obj)
//        {
//            _innerobj = obj;
//        }
//    }


//    public class rbfreport : alglibobject
//    {
//        //
//        // Public declarations
//        //
//        public int arows { get { return _innerobj.arows; } set { _innerobj.arows = value; } }
//        public int acols { get { return _innerobj.acols; } set { _innerobj.acols = value; } }
//        public int annz { get { return _innerobj.annz; } set { _innerobj.annz = value; } }
//        public int iterationscount { get { return _innerobj.iterationscount; } set { _innerobj.iterationscount = value; } }
//        public int nmv { get { return _innerobj.nmv; } set { _innerobj.nmv = value; } }
//        public int terminationtype { get { return _innerobj.terminationtype; } set { _innerobj.terminationtype = value; } }

//        public rbfreport()
//        {
//            _innerobj = new rbf.rbfreport();
//        }

//        //
//        // Although some of declarations below are public, you should not use them
//        // They are intended for internal use only
//        //
//        private rbf.rbfreport _innerobj;
//        public rbf.rbfreport innerobj { get { return _innerobj; } }
//        public rbfreport(rbf.rbfreport obj)
//        {
//            _innerobj = obj;
//        }
//    }






//    public class nearestneighbor {
        
//        public static void kdtreebuildtagged(double[,] xy,
//            int[] tags,
//            int n,
//            int nx,
//            int ny,
//            int normtype,
//            kdtree kdt)
//        {
//            int i = 0;
//            int j = 0;
//            int maxnodes = 0;
//            int nodesoffs = 0;
//            int splitsoffs = 0;
//            int i_ = 0;
//            int i1_ = 0;

//            alglib.ap.assert(n>=0, "KDTreeBuildTagged: N<0");
//            alglib.ap.assert(nx>=1, "KDTreeBuildTagged: NX<1");
//            alglib.ap.assert(ny>=0, "KDTreeBuildTagged: NY<0");
//            alglib.ap.assert(normtype>=0 && normtype<=2, "KDTreeBuildTagged: incorrect NormType");
//            alglib.ap.assert(alglib.ap.rows(xy)>=n, "KDTreeBuildTagged: rows(X)<N");
//            alglib.ap.assert(alglib.ap.cols(xy)>=nx+ny || n==0, "KDTreeBuildTagged: cols(X)<NX+NY");
//            alglib.ap.assert(apserv.apservisfinitematrix(xy, n, nx+ny), "KDTreeBuildTagged: XY contains infinite or NaN values");
            
//            //
//            // initialize
//            //
//            kdt.n = n;
//            kdt.nx = nx;
//            kdt.ny = ny;
//            kdt.normtype = normtype;
//            kdt.kcur = 0;
            
//            //
//            // N=0 => quick exit
//            //
//            if( n==0 )
//            {
//                return;
//            }
            
//            //
//            // Allocate
//            //
//            kdtreeallocdatasetindependent(kdt, nx, ny);
//            kdtreeallocdatasetdependent(kdt, n, nx, ny);
            
//            //
//            // Initial fill
//            //
//            for(i=0; i<=n-1; i++)
//            {
//                for(i_=0; i_<=nx-1;i_++)
//                {
//                    kdt.xy[i,i_] = xy[i,i_];
//                }
//                i1_ = (0) - (nx);
//                for(i_=nx; i_<=2*nx+ny-1;i_++)
//                {
//                    kdt.xy[i,i_] = xy[i,i_+i1_];
//                }
//                kdt.tags[i] = tags[i];
//            }
            
//            //
//            // Determine bounding box
//            //
//            for(i_=0; i_<=nx-1;i_++)
//            {
//                kdt.boxmin[i_] = kdt.xy[0,i_];
//            }
//            for(i_=0; i_<=nx-1;i_++)
//            {
//                kdt.boxmax[i_] = kdt.xy[0,i_];
//            }
//            for(i=1; i<=n-1; i++)
//            {
//                for(j=0; j<=nx-1; j++)
//                {
//                    kdt.boxmin[j] = Math.Min(kdt.boxmin[j], kdt.xy[i,j]);
//                    kdt.boxmax[j] = Math.Max(kdt.boxmax[j], kdt.xy[i,j]);
//                }
//            }
            
//            //
//            // prepare tree structure
//            // * MaxNodes=N because we guarantee no trivial splits, i.e.
//            //   every split will generate two non-empty boxes
//            //
//            maxnodes = n;
//            kdt.nodes = new int[splitnodesize*2*maxnodes];
//            kdt.splits = new double[2*maxnodes];
//            nodesoffs = 0;
//            splitsoffs = 0;
//            for(i_=0; i_<=nx-1;i_++)
//            {
//                kdt.curboxmin[i_] = kdt.boxmin[i_];
//            }
//            for(i_=0; i_<=nx-1;i_++)
//            {
//                kdt.curboxmax[i_] = kdt.boxmax[i_];
//            }
//            kdtreegeneratetreerec(kdt, ref nodesoffs, ref splitsoffs, 0, n, 8);
//        }


//        public static int kdtreequeryknn(kdtree kdt,
//            double[] x,
//            int k,
//            bool selfmatch)
//        {
//            int result = 0;

//            alglib.ap.assert(k>=1, "KDTreeQueryKNN: K<1!");
//            alglib.ap.assert(alglib.ap.len(x)>=kdt.nx, "KDTreeQueryKNN: Length(X)<NX!");
//            alglib.ap.assert(isfinitevector(x, kdt.nx), "KDTreeQueryKNN: X contains infinite or NaN values!");
//            result = kdtreequeryaknn(kdt, x, k, selfmatch, 0.0);
//            return result;
//        }

//        public static void kdtreequeryresultsdistances(kdtree kdt,
//            ref double[] r)
//        {
//            int i = 0;
//            int k = 0;

//            if( kdt.kcur==0 )
//            {
//                return;
//            }
//            if( alglib.ap.len(r)<kdt.kcur )
//            {
//                r = new double[kdt.kcur];
//            }
//            k = kdt.kcur;
            
//            //
//            // unload norms
//            //
//            // Abs() call is used to handle cases with negative norms
//            // (generated during KFN requests)
//            //
//            if( kdt.normtype==0 )
//            {
//                for(i=0; i<=k-1; i++)
//                {
//                    r[i] = Math.Abs(kdt.r[i]);
//                }
//            }
//            if( kdt.normtype==1 )
//            {
//                for(i=0; i<=k-1; i++)
//                {
//                    r[i] = Math.Abs(kdt.r[i]);
//                }
//            }
//            if( kdt.normtype==2 )
//            {
//                for(i=0; i<=k-1; i++)
//                {
//                    r[i] = Math.Sqrt(Math.Abs(kdt.r[i]));
//                }
//            }
//        }


        
        
//        public class kdtree : algopascalobject
//        {
//            public int n;
//            public int nx;
//            public int ny;
//            public int normtype;
//            public double[,] xy;
//            public int[] tags;
//            public double[] boxmin;
//            public double[] boxmax;
//            public int[] nodes;
//            public double[] splits;
//            public double[] x;
//            public int kneeded;
//            public double rneeded;
//            public bool selfmatch;
//            public double approxf;
//            public int kcur;
//            public int[] idx;
//            public double[] r;
//            public double[] buf;
//            public double[] curboxmin;
//            public double[] curboxmax;
//            public double curdist;
//            public int debugcounter;
//            public kdtree()
//            {
//                init();
//            }
//            public override void init()
//            {
//                xy = new double[0,0];
//                tags = new int[0];
//                boxmin = new double[0];
//                boxmax = new double[0];
//                nodes = new int[0];
//                splits = new double[0];
//                x = new double[0];
//                idx = new int[0];
//                r = new double[0];
//                buf = new double[0];
//                curboxmin = new double[0];
//                curboxmax = new double[0];
//            }
//            public override algopascalobject make_copy()
//            {
//                kdtree _result = new kdtree();
//                _result.n = n;
//                _result.nx = nx;
//                _result.ny = ny;
//                _result.normtype = normtype;
//                _result.xy = (double[,])xy.Clone();
//                _result.tags = (int[])tags.Clone();
//                _result.boxmin = (double[])boxmin.Clone();
//                _result.boxmax = (double[])boxmax.Clone();
//                _result.nodes = (int[])nodes.Clone();
//                _result.splits = (double[])splits.Clone();
//                _result.x = (double[])x.Clone();
//                _result.kneeded = kneeded;
//                _result.rneeded = rneeded;
//                _result.selfmatch = selfmatch;
//                _result.approxf = approxf;
//                _result.kcur = kcur;
//                _result.idx = (int[])idx.Clone();
//                _result.r = (double[])r.Clone();
//                _result.buf = (double[])buf.Clone();
//                _result.curboxmin = (double[])curboxmin.Clone();
//                _result.curboxmax = (double[])curboxmax.Clone();
//                _result.curdist = curdist;
//                _result.debugcounter = debugcounter;
//                return _result;
//            }
//        };



//    }

//    public class math
//    {
//        //public static System.Random RndObject = new System.Random(System.DateTime.Now.Millisecond);
//        public static System.Random rndobject = new System.Random(System.DateTime.Now.Millisecond + 1000*System.DateTime.Now.Second + 60*1000*System.DateTime.Now.Minute);

//        public const double machineepsilon = 5E-16;
//        public const double maxrealnumber = 1E300;
//        public const double minrealnumber = 1E-300;
        
//        public static bool isfinite(double d)
//        {
//            return !System.Double.IsNaN(d) && !System.Double.IsInfinity(d);
//        }
        
//        public static double randomreal()
//        {
//            double r = 0;
//            lock(rndobject){ r = rndobject.NextDouble(); }
//            return r;
//        }
//        public static int randominteger(int N)
//        {
//            int r = 0;
//            lock(rndobject){ r = rndobject.Next(N); }
//            return r;
//        }
//        public static double sqr(double X)
//        {
//            return X*X;
//        }        

//        //public static double abscomplex(complex z)
//        //{
//        //    double w;
//        //    double xabs;
//        //    double yabs;
//        //    double v;
    
//        //    xabs = System.Math.Abs(z.x);
//        //    yabs = System.Math.Abs(z.y);
//        //    w = xabs>yabs ? xabs : yabs;
//        //    v = xabs<yabs ? xabs : yabs; 
//        //    if( v==0 )
//        //        return w;
//        //    else
//        //    {
//        //        double t = v/w;
//        //        return w*System.Math.Sqrt(1+t*t);
//        //    }
//        //}
//        //public static complex conj(complex z)
//        //{
//        //    return new complex(z.x, -z.y); 
//        //}    
//        //public static complex csqr(complex z)
//        //{
//        //    return new complex(z.x*z.x-z.y*z.y, 2*z.x*z.y); 
//        //}

//    }
    


//    public abstract class alglibobject
//    {
//        public virtual void _deallocate() {}
//    }


//    public abstract class algopascalobject
//    {
//        public abstract void init();
//        public abstract algopascalobject make_copy();
//    }
//}
